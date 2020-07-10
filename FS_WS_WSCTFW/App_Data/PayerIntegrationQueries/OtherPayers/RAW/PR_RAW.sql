
SELECT ATran.id AS 'Id',
       pay.bucket_mapping_id AS 'PayerId',

    (SELECT REGION
     FROM PBMM..provider x WITH (nolock)
     WHERE x.id = ATran.provider_id) AS 'Region',
       '0' AS BatchId,
       (CASE
            WHEN PRH.xml_file_name IS NOT NULL THEN PRH.xml_file_name
            WHEN PRH.xml_file_name IS NULL THEN POC.prior_request_file_name
        END) AS 'FileName' ,
    (SELECT
         (SELECT PRH.sender_ID AS 'SenderID',
                 PRH.receiver_ID AS 'ReceiverID',
                 Convert(varchar(10),PRH.transaction_date,103) +' '+ Convert(varchar(5),PRH.transaction_date,8) AS 'TransactionDate',
                 PRH.record_count AS 'RecordCount',
                 PRH.disposition_flag AS 'DispositionFlag'
          FOR XML path('Header'),
                  TYPE) ,
         (SELECT pr.[type] AS 'Type',
                 ATran.request_id AS 'ID',
                 PR.member_id AS 'MemberID',
                 (CASE
                      WHEN Pay.license_no IS NULL THEN pay.dubai_license_no
                      WHEN Pay.license_no = '' THEN Pay.dubai_license_no
                      ELSE Pay.license_no
                  END) AS 'PayerID',
                 pr.emirates_id AS 'EmiratesIDNumber',
                 'PBM_' + cast(ATran.id AS varchar(1000))+ '_' + cast(ATran.PCN_code AS varchar(1000)) AS 'IDPayer',
                 Convert(varchar(10),pr.date_ordered,103) AS 'DateOrdered' ,
              (SELECT PRE.facility_ID AS FacilityID,
                      PRE.[type] AS 'Type'
               FROM PBMM..PRIOR_REQUEST_ENCOUNTER PRE WITH (Nolock)
               WHERE PRE.prior_Request_id = pr.id
                   FOR xml path('Encounter'),
                           TYPE ) ,
              (SELECT PRD.[type] AS 'Type',
                      (CASE
                           WHEN PRD.mappedICD10_code IS NOT NULL THEN PRD.mappedICD10_code
                           ELSE PRD.code
                       END) AS Code
               FROM PBMM..PRIOR_REQUEST_DIAGNOSIS PRD WITH (Nolock)
               WHERE PRD.prior_Request_id = pr.id
                   FOR xml path('Diagnosis'),
                           TYPE ) ,
              (SELECT ID AS ID,
                      Convert(varchar(10),start_date,103) +' '+ Convert(varchar(5),start_date,8) AS 'Start',
                      TYPE AS 'Type',
                              code AS Code,
                              (CASE
                                   WHEN P.Region = 2 THEN (Convert(decimal(10,2),PRA.pbm_quantity))
                                   WHEN P.Region != 2 THEN (Convert(decimal(10,2),PRA.quantity))
                               END) AS 'Quantity',
                              Convert(decimal(10,2),net) AS 'Net',
                              Clinician ,
                   (SELECT [type] AS 'Type',
                           [code] AS Code,
                           [value] AS Value,
                           [value_type] AS ValueType
                    FROM PBMM..PRIOR_REQUEST_ACT_OBSERVATION PRAO WITH (Nolock)
                    WHERE PRAO.request_id = PR.id
                        AND PRA.id = PRAO.Activity_ID
                        FOR xml path('Observation'),
                                TYPE )
               FROM PBMM..PRIOR_REQUEST_ACTIVITY PRA WITH (Nolock)
               WHERE PRA.prior_Request_id = PR.id
                   FOR xml path('Activity'),
                           TYPE ) , PRR.type AS 'ResubType',
                                    PRR.comment AS 'ResubComment',
                                    PRR.attachment AS 'ResubAttach'
          FOR xml path('Authorization'),
                  TYPE)
     FOR xml path ('Prior.Request'),
             TYPE)
FROM PBMM..AUTHORIZATION_TRANSACTION ATran with(nolock)
INNER JOIN PBMM..PRIOR_REQUEST PR WITH (Nolock) ON ATran.id = pr.id
INNER JOIN PBMM..[Payer] Pay WITH (nolock) ON pay.id = ATran.payer_id
INNER JOIN PBMM..PRIOR_REQUEST_HEADER PRH WITH (Nolock) ON PRH.prior_request_id = PR.id
INNER JOIN PBMM..[PROVIDER] P WITH (nolock) ON P.id = ATran.provider_id
INNER JOIN PBMM..[MEMBER] M WITH (nolock) ON M.id = ATran.member_id
LEFT JOIN PBMM..POST_OFFICE_COMM POC WITH (Nolock) ON POC.Trans_ID = ATran.id
LEFT JOIN PBMM..PRIOR_REQUEST_RESUBMISSION PRR WITH (Nolock) ON PRR.request_id = PR.id
WHERE ATran.id = 34324433
    FOR xml path ('Request'),
            TYPE