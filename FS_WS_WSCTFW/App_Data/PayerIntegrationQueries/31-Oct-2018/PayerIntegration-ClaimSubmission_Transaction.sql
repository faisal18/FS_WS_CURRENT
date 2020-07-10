Declare @TranCount int
Set @TranCount = 1000

declare @Payerid int
set @Payerid = 7 

declare @region int
set @region = 3

Declare @TransactionID int
--Declare @Server varchar (100)
--set @Server = '10.156.176.85'
--Declare @DBName varchar(100)
--set @DBName = 'PBMM'
--declare @loginId varchar(100)
--set @loginId = 'fsheikh' 
--declare @Password varchar(100)
--set @Password = 'Dell@100'

------------------------------------------------------------------------------------------------------------------------
--select top 100 * from REGION
--select top 100 * from PROVIDER
USE [PBMM]

DECLARE TransList CURSOR 
  LOCAL STATIC READ_ONLY FORWARD_ONLY
FOR 

Select top (@TranCount) 
 at.id  
from 
 AUTHORIZATION_TRANSACTION at  with (Nolock) 
 inner join claim_submission CS  with (Nolock) on At.id = CS.id
where 
-- at.payer_id in (select PAY2.id from PBMM..PAYER PAY2 with (nolock) where PAY2.tpa_dubai_code = 'TPA026')
at.payer_id in (select PAY2.id from PBMM..PAYER PAY2 with(nolock) where bucket_mapping_id in (1,3,4,5,7,54,1247,1285,1325,1333,1350,1364))
and CS.download_batch_id in (null, '0')
and at.state_id = 4
and at.created_dt  < DATEADD(HOUR, -1, GETDATE())

order by at.created_dt desc
--and at.provider_id in (select id from [PROVIDER] where Region in (@region))
---------------------------------------------

open TransList 
FETCH NEXT FROM Translist INTO @TransactionID


WHILE @@FETCH_STATUS = 0
BEGIN 
    
    PRINT @TransactionID
   
   -------------------------------------------------



   

declare @SQL varchar(max)
set @SQL = 'Select '+
' AT.id as ''Id'','+
' PAY2.bucket_mapping_id as ''PayerId'','+
' Prov.Region  as ''Region'','+
'''0'' as ''BatchId'','+
'(CASE when CSH.xml_file_name is not null then CSH.xml_file_name when CSH.xml_file_name != '''' then CSH.xml_file_name ELSE POC.claim_sub_file_name END) as ''FileName'','+
' ( Select '+
'	(Select '+
'		(SELECT PROV.license_no WHERE PROV.id = CSH.sender_id) as ''SenderID'','+
'		(CASE WHEN PAY2.tpa_code is not null THEN PAY2.tpa_code WHEN PAY2.tpa_code != '''' THEN PAY2.tpa_code ELSE PAY2.tpa_dubai_code  END) as ''ReceiverID'','+
'		Convert(varchar(10),CSH.transaction_date,103) +'' ''+ Convert(varchar(5),CSH.transaction_date,8) as ''TransactionDate'','+
'		CSH.record_count as ''RecordCount'','+
'		CSH.disposition_flag as ''DispositionFlag'''+
'		where CSH.receiver_id = PAY2.id '+
'		for xml Path (''Header''),type'+
'	),'+
'	(Select'+
'		AT.request_id as ''ID'','+
'		''PBM_'' + cast(at.id as varchar(1000) )+ ''_'' + cast(at.PCN_code as varchar(1000)) as ''IDPayer'','+
'		(select member_id from PBMM..PRIOR_REQUEST PR with(nolock) where PR.id = at.id) as ''MemberID'','+
'		(CASE WHEN PAY2.license_no is not null THEN PAY2.license_no WHEN PAY2.license_no != '''' THEN PAY2.license_no ELSE PAY2.dubai_license_no END) as ''PayerID'','+
'		(select PROV.license_no from PBMM..PROVIDER PROV with(nolock) where PROV.id = AT.provider_id) as ''ProviderID'','+
'		(CASE WHEN CS.emiratesIDNumber is not null then CS.emiratesIDNumber WHEN CS.emiratesIDNumber != '''' THEN CS.emiratesIDNumber ELSE ''999-9999-9999999-9'' END ) as ''EmiratesIDNumber'','+
'		Convert(decimal(10,2),CS.gross) as ''Gross'','+
'		Convert(decimal(10,2),CS.patientShare) as ''PatientShare'','+
'		Convert(decimal(10,2),CS.net) as ''Net'','+
'		(Select '+
'			Prov.license_no as ''FacilityID'','+
'			CSE.type as ''Type'','+
'			CSE.patient_id as ''PatientID'','+
'			(case when CSE.start_date is not null then (Convert(varchar(10),CSE.start_date,103) +'' ''+ Convert(varchar(5),CSE.start_date,8)) ELSE (Convert(varchar(10),GETDATE(),103)+'' ''+Convert(varchar(5),GETDATE(),8)) END) as ''Start'','+
'			Convert(varchar(10),CSE.end_date,103) +'' ''+ Convert(varchar(5),CSE.end_date,8) as ''End'','+
'			CSE.start_type as ''StartType'','+
'			CSE.end_type as ''EndType'''+
'			from PBMM..CLAIM_SUBMISSION_ENCOUNTER CSE with (nolock)'+
'			where CSE.submission_id = at.id'+
'			for xml path(''Encounter''),type	'+
'		),'+
'		(Select'+
'			PRD.type as ''Type'','+
'			(Case when PRD.mappedICD10_code is not null then PRD.mappedICD10_code when PRD.mappedICD10_code != '''' then PRD.mappedICD10_code ELSE PRD.code END) as ''Code'''+
'			from PBMM..PRIOR_REQUEST_DIAGNOSIS PRD with (nolock)'+
'			inner join PBMM..PRIOR_REQUEST PR with(nolock)'+
'			on PRD.prior_request_id = PR.id '+
'			where PR.id = AT.id'+
'			for xml path(''Diagnosis''),type '+
'		),'+
'		(Select'+
'			CSA.id as ''ID'','+
'			(case when CSA.start_date is not null then (Convert(varchar(10),CSA.start_date,103) +'' ''+ Convert(varchar(5),CSA.start_date,8)) ELSE (Convert(varchar(10),GETDATE(),103)+'' ''+Convert(varchar(5),GETDATE(),8)) END) as ''Start'','+
'			CSA.type as ''Type'','+
'			CSA.code as ''Code'','+
'			(CASE when Prov.Region = 2 then Convert(decimal(10,2),(CSA.quantity * (select DCR.granual_units from PBMM..DRUG_CODE_REF DCR with (nolock) where DCR.id = CSA.drug_id))) when Prov.Region != 2 then Convert(decimal(10,2),(CSA.quantity)) END) as ''Quantity'','+
'			Convert(decimal(10,2),CSA.net) as ''Net'','+
'			(CASE WHEN Prov.Region = 1 THEN CSA.clinician END) as ''OrderingClinician'','+
'			CSA.clinician as ''Clinician'','+
'			''PBM_'' + cast(at.id as varchar(1000) )+ ''_'' + cast(at.PCN_code as varchar(1000)) as ''PriorAuthorizationID'','+
'			(Select '+
'				CSAO.type as ''Type'','+
'				CSAO.code as ''Code'','+
'				CSAO.value as ''Value'','+
'				REPLACE(REPLACE(CSAO.value_type, CHAR(13), ''''), CHAR(10), '''') as ''ValueType'''+
'				from PBMM..CLAIM_SUBMIT_ACT_OBSERVATION CSAO with (nolock)'+
'				where CSAO.submission_id = at.id and CSA.id =csao.activity_id'+
'				for xml path(''Observation''),type'+
'			)'+
'			from PBMM..CLAIM_SUBMISSION_ACTIVITY CSA with(nolock)'+
'			where CSA.submission_id = at.id'+
'			for xml path(''Activity''),type'+
'		),'+
'		(Select '+
'			CSR.type as ''Type'','+
'			CSR.comment as ''Comment'','+
'			CSR.attachment as ''Attachments'''+
'			from PBMM..CLAIM_SUBMIT_RESUBMISSION CSR with(nolock)'+
'			where CSR.submission_id = AT.id'+
'			for xml path(''Resubmission''),type'+
'		)'+
'		from PBMM..CLAIM_SUBMISSION CS with (NoLock)'+
'		where CS.id = AT.id'+
'		for xml Path(''Claim''),type'+
'	)'+
' for xml Path(''Claim.Submission''),type'+
' )'+
' from PBMM..AUTHORIZATION_TRANSACTION [AT]'+
' INNER JOIN PBMM..PROVIDER Prov with (NOLOCK) ON Prov.id = AT.provider_id'+
' left Join PBMM..POST_OFFICE_COMM POC with (nolock) ON POC.trans_id = [AT].id'+
' left join PBMM..CLAIM_SUBMISSION CS with (nolock) ON CS.id = [AT].id'+
' inner join PBMM..Payer PAY2 with (nolock) ON PAY2.id = [AT].payer_id'+
' INNER JOIN PBMM..CLAIM_SUBMISSION_HEADER CSH with (NoLock) ON CSH.submission_id = [AT].id '+
' where [AT].id =  '+ CAST(@TransactionID as varchar(1000)) +
' for XML Path(''Claim'') ' --,Root('''')'

declare @filename varchar(1000)
set @filename = ''
set @filename = 'C:\tmp\CS\CS_' + cast(@transactionID as varchar(1000))+ '.XML'
--set @filename = 'C:\PayerIntegration\Claim\NonProcessed\CS_' + cast(@transactionID as varchar(1000))+ '.XML'
--set @filename = 'C:\tmp\Aafia\CS\CS_' + cast(@transactionID as varchar(1000))+ '.XML'


--select @SQL
--sqlcmd -S @server -U loginid -P @LoginID -d @DbName -Q @SQL -o @filename
--set @sql = 'select getdate()'
declare @BCP1 varchar(8000) 
--select @BCP1 
set @bcp1 = 'bcp "'+@SQL+'" QUERYOUT "'+@filename+'" -C -c -T -t,'
--'bcp '+ '" select getdate ()"' + ' QUERYOUT "C:\tmp\FILE_'+Cast(12 AS Varchar)+'.CSV" -C -c -T'
--select @BCP1 
--' bcp "'+ @sql  +'" queryout "' + @filename +'" -C -c -T  '
--	select @BCP1
EXEC xp_cmdshell @BCP1
   ----------------------------------------------------

    update top (1) CLAIM_SUBMISSION
	set download_batch_id = 'Picked'
	where id = @TransactionID
	Print 'Transaction Status updated successfully'
   
    FETCH NEXT FROM Translist INTO @TransactionID

END
CLOSE TransList
DEALLOCATE TransList
