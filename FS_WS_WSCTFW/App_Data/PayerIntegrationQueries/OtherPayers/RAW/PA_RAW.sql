
-- AUTH START -- 
SELECT AT.id AS 'Id',
       PAY.bucket_mapping_id AS 'PayerId',
       prov.REGION AS 'Region',
       '0' AS 'BatchId',
       (CASE WHEN PAH.xml_file_name IS NOT NULL THEN PAH.xml_file_name ELSE POC.prior_auth_file_name END) AS 'FileName',
	   -- PA START -- 
    (SELECT
		-- HEADER START --
         (SELECT PAY.license_no AS 'SenderID',
                 PAH.receiver_id AS 'ReceiverID',
                 Convert(varchar(10),PAH.transaction_date,103) +' '+ Convert(varchar(5),PAH.transaction_date,8) AS 'TransactionDate',
                 PAH.record_count AS 'RecordCount',
                 PAH.disposition_flag AS 'DispositionFlag'
          FOR XML PATH ('Header'),TYPE),
		  -- HEADER END --

		  -- AUTHORIZATION START --
         (SELECT (CASE WHEN pa.result = 'Accepted' THEN 'Yes' WHEN pa.result = 'Rejected' THEN 'No' END) AS 'Result',
                 AT.request_id AS 'ID',
                 'PBM_' + cast(at.id AS varchar(1000))+ '_' + cast(at.PCN_code AS varchar(1000)) AS 'IDPayer',
                 Convert(varchar(10),PA.start_date,103) +' '+ Convert(varchar(5),PA.start_date,8) AS 'Start',
                 Convert(varchar(10),PA.end_date,103) +' '+ Convert(varchar(5),PA.end_date,8) AS 'End',
                 CAST((SELECT 'MemberID: (' + CAST(MEM2.MI_memberNo AS nvarchar) +')'
                        FROM PBMM..MEMBER MEM2 with(nolock)
                        WHERE MEM2.id = AT.member_id) AS nvarchar(MAX)) + CAST((SELECT (' Activity(' + CAST(PAAD2.activity_id AS nvarchar) + ',Claim Ref ID: ' + CAST(PAA2.pbm_claim_id AS nvarchar) + ') ' + CAST(PAAD2.comment AS nvarchar(MAX)) + ' (' + CAST(PAAD2.denial_code AS nvarchar) + ')')
						FROM PBMM..PRIOR_AUTH_ACTIVITY PAA2 WITH (nolock)
						LEFT JOIN PBMM..PRIOR_AUTH_ACTIVITY_DENIAL PAAD2 with(nolock) ON PAAD2.authorization_id = AT.id
						AND PAAD2.activity_id = PAA2.id
						WHERE PAA2.authorization_id = AT.id
						FOR xml path(''),TYPE) AS nvarchar(MAX)) AS 'Comments',

				-- ACTIVITY START -- 

				(SELECT PAA.activity_id AS 'ID',
                      PAA.type AS 'Type',
                      PAA.drug_code AS 'Code',
                      (CASE
                           WHEN Prov.Region = 2 THEN Convert(decimal(10,2),(PAA.drug_quantity *
                            (SELECT DCR.granual_units
                                FROM PBMM..DRUG_CODE_REF DCR WITH (nolock)
                                WHERE DCR.id = PAA.drug_id)))
								WHEN Prov.Region != 2 THEN Convert(decimal(10,2),(PAA.drug_quantity))
                       END) AS 'Quantity',
                      Convert(decimal(10,2),PAA.drug_net) AS 'Net',
                      Convert(decimal(10,2),PAA.drug_list_price) AS 'List',
                      Convert(decimal(10,2),PAA.patient_share) AS 'PatientShare',
                      Convert(decimal(10,2),PAA.payment_amount) AS 'PaymentAmount'
                      ,((CASE
                            WHEN prov.license_no IS NULL OR prov.license_no = '' THEN 'AUTH-005'
                            ELSE
                            (SELECT DC.pbm_denial_code
                            FROM PBMM..DENIAL_CODE DC WITH (nolock)
                            INNER JOIN PBMM..PRIOR_AUTH_ACTIVITY_DENIAL PAAD2 WITH (nolock) ON PAAD2.denial_code = DC.pbm_denial_code
                            AND PAAD2.activity_id = PAA.id
                            AND PAAD2.authorization_id = AT.id
							)
                        END)) AS 'DenialCode' 

							-- OBSERVATION START --
							,(SELECT 'Text' AS 'Type',
									'Requested_Net' AS 'Code',
									Convert(decimal(10,2),PAA.drug_gross) AS 'Value',
									'Requested_Net' AS 'ValueType'
									FOR XML PATH ('Observation'),TYPE),

							(SELECT 'Text' AS 'Type',
									'MemberNo' AS 'Code',
									(SELECT member_no FROM PBMM..MEMBER MEM WITH (nolock) WHERE MEM.id = AT.member_id) AS 'Value',
									'MemberNo' AS 'ValueType'
							FOR XML PATH ('Observation'),TYPE),

							(SELECT 'Text' AS 'Type',
									'MiMemberNo' AS 'Code',
							(SELECT MI_memberNo
								FROM PBMM..MEMBER MEM WITH (nolock)
								WHERE MEM.id = AT.member_id) AS 'Value',
								'MiMemberNo' AS 'ValueType' 
								FOR XML PAtH('Observation'),TYPE) 
						
								,(CASE
								WHEN PAA.denial_code IS NOT NULL THEN
								(SELECT
									(SELECT 'Text' AS 'Type',
											'PBM_Rejection_Code' AS 'Code',
											PAAD.denial_code AS 'Value',
											'PBM_Rejection_Code' AS 'ValueType'
									FOR XML PATH('Observation'),TYPE)
									,(SELECT 'Text' AS 'Type',
											'Comments' AS 'Code',
											REPLACE(REPLACE(PAAD.comment, CHAR(13), ''), CHAR(10), '') AS 'Value',
											'Comments' AS 'ValueType'
									FOR XML PATH ('Observation'),TYPE)
									

								FROM PBMM..PRIOR_AUTH_ACTIVITY_DENIAL PAAD WITH (nolock)
								WHERE PAAD.activity_id = PAA.id
								AND PAAD.authorization_id = PAA.authorization_id
								FOR XML PATH (''),TYPE)
								END)
								-- OBSERVATION END --
						
               FROM PBMM..PRIOR_AUTH_ACTIVITY PAA WITH (nolock)
               WHERE PAA.authorization_id = PA.id
               FOR XML PATH ('Activity'),TYPE)
			   -- ACTIVITY END
          FROM PBMM..PRIOR_AUTHORIZATION PA WITH (nolock)
          WHERE PA.id = AT.id
          FOR XML PATH ('Authorization'),TYPE)
		  -- AUTHORIZATION END -- 

		  -- PA END -- 
     FOR XML PATH('Prior.Authorization'),TYPE)
FROM PBMM..AUTHORIZATION_TRANSACTION [AT]
INNER JOIN PBMM..provider prov WITH (nolock) ON prov.id = AT.provider_id
INNER JOIN PBMM..PRIOR_AUTH_HEADER PAH with(NOLOCK) ON PAH.authorization_id = AT.id
LEFT JOIN PBMM..POST_OFFICE_COMM POC WITH (nolock) ON POC.trans_id = at.id
INNER JOIN PBMM..PAYER PAY WITH (nolock) ON PAY.id = AT.payer_id
WHERE at.id = 33884920
FOR XML PATH('Authorization'),TYPE

-- AUTH END -- 