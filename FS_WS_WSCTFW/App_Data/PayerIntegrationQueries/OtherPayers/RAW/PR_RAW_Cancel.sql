
SELECT ATran.id AS 'Id',
       Pay.bucket_mapping_id AS 'PayerId',
       P.REGION AS 'Region',
       '0' AS 'BatchId',
       (CASE
            WHEN PRH.xml_file_name IS NOT NULL THEN PRH.xml_file_name
            ELSE POC.prior_request_file_name
        END) AS 'FileName',
    (SELECT
         (SELECT PRH.sender_ID AS 'SenderID',
                 PRH.receiver_ID AS 'ReceiverID',
                 Convert(varchar(10),PRH.transaction_date,103) +' '+ Convert(varchar(5),PRH.transaction_date,8) AS 'TransactionDate',
                 PRH.record_count AS 'RecordCount',
                 PRH.disposition_flag AS 'DispositionFlag'
          FOR XML PATH ('Header'),
                  TYPE),
         (SELECT 'Cancellation' AS 'Type',
                 ATran.request_id AS 'ID',
                 PR.member_id AS 'MemberID',
                 (CASE
                      WHEN Pay.license_no IS NULL THEN pay.dubai_license_no
                      WHEN Pay.license_no = '' THEN Pay.dubai_license_no
                      ELSE Pay.license_no
                  END) AS 'PayerID',
                 pr.emirates_id AS 'EmiratesIDNumber',
                 'PBM_' + cast(ATran.id AS varchar(1000))+ '_' + cast(ATran.PCN_code AS varchar(1000)) AS 'IDPayer',
                 Convert(varchar(10),pr.date_ordered,103) AS 'DateOrdered',
              (SELECT PRE.facility_ID AS 'FacilityID',
                      PRE.type AS 'Type'
               FROM PBMM..PRIOR_REQUEST_ENCOUNTER PRE WITH (Nolock)
               WHERE PRE.prior_request_id = ATran.id
                   FOR XML PATH ('Encounter'),
                           TYPE ),
              (SELECT PRD.type AS 'Type',
                      (CASE
                           WHEN PRD.mappedICD10_code IS NOT NULL THEN PRD.mappedICD10_code
                           ELSE PRD.code
                       END) AS 'Code'
               FROM PBMM..PRIOR_REQUEST_DIAGNOSIS PRD WITH (Nolock)
               WHERE PRD.prior_request_id = ATran.id
                   FOR XML PATH ('Diagnosis'),
                           TYPE ),
              (SELECT PRA.activity_ID AS 'ID',
                      Convert(varchar(10),PRA.start_date,103) +' '+ Convert(varchar(5),PRA.start_date,8)AS 'Start',
                      PRA.type AS 'Type',
                      PRA.code AS 'Code',
                      (CASE
                           WHEN P.Region = 2 THEN (Convert(decimal(10,2),PRA.pbm_quantity))
                           WHEN P.Region != 2 THEN (Convert(decimal(10,2),PRA.quantity))
                       END) AS 'Quantity',
                      Convert(decimal(10,2),PRA.net) AS 'Net',
                      PRA.clinician AS 'Clinician',
                   (SELECT PRAO.type AS 'Type',
                           PRAO.code AS 'Code',
                           PRAO.value AS 'Value',
                           PRAO.value_type AS 'ValueType'
                    FROM PBMM..PRIOR_REQUEST_ACT_OBSERVATION PRAO WITH (Nolock)
                    WHERE PRAO.request_id = ATran.id
                        AND PRAO.activity_id = PRA.ID
                        FOR XML PATH ('Observation'),
                                TYPE )
               FROM PBMM..PRIOR_REQUEST_ACTIVITY PRA WITH (Nolock)
               WHERE PRA.prior_request_id = ATran.id
                   FOR XML PATH ('Activity'),
                           TYPE )
          FOR XML PATH('Authorization'),
                  TYPE)
     FOR XML PATH('Prior.Request'),
             TYPE)
FROM PBMM..AUTHORIZATION_TRANSACTION ATran with(nolock)
INNER JOIN PBMM..PRIOR_REQUEST PR WITH (Nolock) ON ATran.id = pr.id
INNER JOIN PBMM..[Payer] Pay WITH (nolock) ON pay.id = ATran.payer_id
INNER JOIN PBMM..PRIOR_REQUEST_HEADER PRH WITH (Nolock) ON PRH.prior_request_id = PR.id
INNER JOIN PBMM..[PROVIDER] P WITH (nolock) ON P.id = ATran.provider_id
INNER JOIN PBMM..[MEMBER] M WITH (nolock) ON M.id = ATran.member_id
LEFT JOIN PBMM..POST_OFFICE_COMM POC WITH (Nolock) ON POC.Trans_ID = ATran.id
LEFT JOIN PBMM..PRIOR_REQUEST_RESUBMISSION PRR WITH (Nolock) ON PRR.request_id = PR.id
WHERE ATran.id = 34316985
    FOR XML PATH(''),
            ROOT('Cancel')