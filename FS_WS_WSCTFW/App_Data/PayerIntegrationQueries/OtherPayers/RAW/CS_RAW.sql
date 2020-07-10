SELECT AT.id AS 'Id',
       PAY2.bucket_mapping_id AS 'PayerId',
       Prov.Region AS 'Region',
       '0' AS 'BatchId',
       (CASE
            WHEN CSH.xml_file_name IS NOT NULL THEN CSH.xml_file_name
            WHEN CSH.xml_file_name != '' THEN CSH.xml_file_name
            ELSE POC.claim_sub_file_name
        END) AS 'FileName',
    (SELECT
         (SELECT
              (SELECT PROV.license_no
               WHERE PROV.id = CSH.sender_id) AS 'SenderID',
                 (CASE
                      WHEN PAY2.tpa_code IS NOT NULL THEN PAY2.tpa_code
                      WHEN PAY2.tpa_code != '' THEN PAY2.tpa_code
                      ELSE PAY2.tpa_dubai_code
                  END) AS 'ReceiverID',
                 Convert(varchar(10),CSH.transaction_date,103) +' '+ Convert(varchar(5),CSH.transaction_date,8) AS 'TransactionDate',
                 CSH.record_count AS 'RecordCount',
                 CSH.disposition_flag AS 'DispositionFlag'
          WHERE CSH.receiver_id = PAY2.id
              FOR xml Path ('Header'),
                      TYPE ),
         (SELECT AT.id AS 'ID',
                 'PBM_' + cast(at.id AS varchar(1000))+ '_' + cast(at.PCN_code AS varchar(1000)) AS 'IDPayer',

              (SELECT member_id
               FROM PBMM..PRIOR_REQUEST PR with(nolock)
               WHERE PR.id = at.id) AS 'MemberID',
                 (CASE
                      WHEN PAY2.license_no IS NOT NULL THEN PAY2.license_no
                      WHEN PAY2.license_no != '' THEN PAY2.license_no
                      ELSE PAY2.dubai_license_no
                  END) AS 'PayerID',

              (SELECT PROV.license_no
               FROM PBMM..PROVIDER PROV with(nolock)
               WHERE PROV.id = AT.provider_id) AS 'ProviderID',
                 (CASE
                      WHEN CS.emiratesIDNumber IS NOT NULL THEN CS.emiratesIDNumber
                      WHEN CS.emiratesIDNumber != '' THEN CS.emiratesIDNumber
                      ELSE '999-9999-9999999-9'
                  END) AS 'EmiratesIDNumber',
                 Convert(decimal(10,2),CS.gross) AS 'Gross',
                 Convert(decimal(10,2),CS.patientShare) AS 'PatientShare',
                 Convert(decimal(10,2),CS.net) AS 'Net',
              (SELECT Prov.license_no AS 'FacilityID',
                      CSE.type AS 'Type',
                      CSE.patient_id AS 'PatientID',
                      (CASE
                           WHEN CSE.start_date IS NOT NULL THEN (Convert(varchar(10),CSE.start_date,103) +' '+ Convert(varchar(5),CSE.start_date,8))
                           ELSE (Convert(varchar(10),GETDATE(),103)+' '+Convert(varchar(5),GETDATE(),8))
                       END) AS 'Start',
                      Convert(varchar(10),CSE.end_date,103) +' '+ Convert(varchar(5),CSE.end_date,8) AS 'End',
                      CSE.start_type AS 'StartType',
                      CSE.end_type AS 'EndType'
               FROM PBMM..CLAIM_SUBMISSION_ENCOUNTER CSE WITH (nolock)
               WHERE CSE.submission_id = at.id
                   FOR xml path('Encounter'),
                           TYPE ),
              (SELECT PRD.type AS 'Type',
                      (CASE
                           WHEN PRD.mappedICD10_code IS NOT NULL THEN PRD.mappedICD10_code
                           WHEN PRD.mappedICD10_code != '' THEN PRD.mappedICD10_code
                           ELSE PRD.code
                       END) AS 'Code'
               FROM PBMM..PRIOR_REQUEST_DIAGNOSIS PRD WITH (nolock)
               INNER JOIN PBMM..PRIOR_REQUEST PR with(nolock) ON PRD.prior_request_id = PR.id
               WHERE PR.id = AT.id
                   FOR xml path('Diagnosis'),
                           TYPE ),
              (SELECT CSA.id AS 'ID',
                      (CASE
                           WHEN CSA.start_date IS NOT NULL THEN (Convert(varchar(10),CSA.start_date,103) +' '+ Convert(varchar(5),CSA.start_date,8))
                           ELSE (Convert(varchar(10),GETDATE(),103)+' '+Convert(varchar(5),GETDATE(),8))
                       END) AS 'Start',
                      CSA.type AS 'Type',
                      CSA.code AS 'Code',
                      (CASE
                           WHEN Prov.Region = 2 THEN Convert(decimal(10,2),(CSA.quantity *
                                                                                (SELECT DCR.granual_units
                                                                                 FROM PBMM..DRUG_CODE_REF DCR WITH (nolock)
                                                                                 WHERE DCR.id = CSA.drug_id)))
                           WHEN Prov.Region != 2 THEN Convert(decimal(10,2),(CSA.quantity))
                       END) AS 'Quantity',
                      Convert(decimal(10,2),CSA.net) AS 'Net',
                      (CASE
                           WHEN Prov.Region = 1 THEN CSA.clinician
                       END) AS 'OrderingClinician',
                      CSA.clinician AS 'Clinician',
                      'PBM_' + cast(at.id AS varchar(1000))+ '_' + cast(at.PCN_code AS varchar(1000)) AS 'PriorAuthorizationID',
                   (SELECT CSAO.type AS 'Type',
                           CSAO.code AS 'Code',
                           CSAO.value AS 'Value',
                           REPLACE(REPLACE(CSAO.value_type, CHAR(13), ''), CHAR(10), '') AS 'ValueType'
                    FROM PBMM..CLAIM_SUBMIT_ACT_OBSERVATION CSAO WITH (nolock)
                    WHERE CSAO.submission_id = at.id
                        AND CSA.id =csao.activity_id
                        FOR xml path('Observation'),
                                TYPE )
               FROM PBMM..CLAIM_SUBMISSION_ACTIVITY CSA with(nolock)
               WHERE CSA.submission_id = at.id
                   FOR xml path('Activity'),
                           TYPE ),
              (SELECT CSR.type AS 'Type',
                      CSR.comment AS 'Comment',
                      CSR.attachment AS 'Attachments'
               FROM PBMM..CLAIM_SUBMIT_RESUBMISSION CSR with(nolock)
               WHERE CSR.submission_id = AT.id
                   FOR xml path('Resubmission'),
                           TYPE )
          FROM PBMM..CLAIM_SUBMISSION CS WITH (NoLock)
          WHERE CS.id = AT.id
              FOR xml Path('Claim'),
                      TYPE )
     FOR xml Path('Claim.Submission'),
             TYPE)
FROM PBMM..AUTHORIZATION_TRANSACTION [AT]
INNER JOIN PBMM..PROVIDER Prov WITH (NOLOCK) ON Prov.id = AT.provider_id
LEFT JOIN PBMM..POST_OFFICE_COMM POC WITH (nolock) ON POC.trans_id = [AT].id
LEFT JOIN PBMM..CLAIM_SUBMISSION CS WITH (nolock) ON CS.id = [AT].id
INNER JOIN PBMM..Payer PAY2 WITH (nolock) ON PAY2.id = [AT].payer_id
INNER JOIN PBMM..CLAIM_SUBMISSION_HEADER CSH WITH (NoLock) ON CSH.submission_id = [AT].id
WHERE [AT].id = 34324378
    FOR XML Path('Claim')