Declare @TranCount int
Set @TranCount = 1000

declare @Payerid table(Payers nvarchar(100))
insert @Payerid(Payers) values (1),(3),(4),(5),(7),(54),(1247),(1285),(1333),(1325),(1350),(1364),(1275)

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
 inner join PRIOR_REQUEST PR  with (Nolock) on At.id = pr.id
where 
-- at.payer_id in (select PAY2.id from PBMM..PAYER PAY2 with (nolock) where PAY2.tpa_dubai_code = 'TPA026')
 at.payer_id in (select PAY2.id from PBMM..PAYER PAY2 with(nolock) where bucket_mapping_id in (select Payers from @Payerid))
and 
PR.download_canceled_batch_id in (null, '0','log','NULL')
and 
at.state_id = 8
and at.created_dt  < DATEADD(HOUR, -1, GETDATE())
and 
pr.[type] = 'Authorization'

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
set @SQL = ' ' +

' Select '+
' ATran.id as ''Id'','+
' Pay.bucket_mapping_id as ''PayerId'','+
' P.REGION  as ''Region'','+
' ''0'' as ''BatchId'','+
' (CASE WHEN PRH.xml_file_name IS NOT NULL THEN PRH.xml_file_name ELSE POC.prior_request_file_name END) as ''FileName'','+
' ('+
' SELECT'+
' ('+
' Select '+
' PRH.sender_ID as ''SenderID'','+
' PRH.receiver_ID as ''ReceiverID'','+
' Convert(varchar(10),PRH.transaction_date,103) +'' ''+ Convert(varchar(5),PRH.transaction_date,8) as ''TransactionDate'','+
' PRH.record_count as ''RecordCount'','+
' PRH.disposition_flag as ''DispositionFlag'''+
' FOR XML PATH (''Header''),TYPE'+
' ),'+
' ('+
' Select'+
' ''Cancellation'' as ''Type'','+
' ATran.request_id as ''ID'','+
' PR.member_id as ''MemberID'','+
' (CASE WHEN Pay.license_no IS NULL THEN pay.dubai_license_no WHEN Pay.license_no = '''' THEN Pay.dubai_license_no ELSE Pay.license_no END) as ''PayerID'','+
' pr.emirates_id as ''EmiratesIDNumber'','+
'  ''PBM_'' + cast(ATran.id as varchar(1000) )+ ''_'' + cast(ATran.PCN_code as varchar(1000)) as ''IDPayer''' + 
' , Convert(varchar(10),pr.date_ordered,103)  as ''DateOrdered'','+
' ('+
' Select '+
' PRE.facility_ID as ''FacilityID'','+
' PRE.type as ''Type'''+
' FROM PBMM..PRIOR_REQUEST_ENCOUNTER PRE  with (Nolock) '+
' where PRE.prior_request_id = ATran.id'+
' FOR XML PATH (''Encounter''),TYPE '+
' ),'+
' ('+
' Select'+
' PRD.type as ''Type'','+
'  (CASE WHEN PRD.mappedICD10_code IS NOT NULL THEN PRD.mappedICD10_code ELSE PRD.code END) as ''Code'''+
' FROM PBMM..PRIOR_REQUEST_DIAGNOSIS PRD  with (Nolock)'+
''+
' where PRD.prior_request_id = ATran.id'+
' FOR XML PATH (''Diagnosis''),TYPE '+
' ),'+
''+
' ('+
' Select PRA.activity_ID as ''ID'','+
' Convert(varchar(10),PRA.start_date,103) +'' ''+ Convert(varchar(5),PRA.start_date,8)as ''Start'', '+
' PRA.type as ''Type'', '+
' PRA.code as ''Code'','+
'(CASE'+
'   WHEN P.Region = 2 THEN (Convert(decimal(10,2),PRA.pbm_quantity))'+
'   WHEN P.Region = 3 AND Pay.bucket_mapping_id = 7 THEN (Convert(decimal(10,2),PRA.pbm_quantity))'+
'   ELSE (Convert(decimal(10,2),PRA.quantity))'+
'END) AS ''Quantity'','+
' Convert(decimal(20,2),PRA.net) as ''Net'', '+
' PRA.clinician as ''Clinician'','+
' ('+
' Select '+
' PRAO.type as ''Type'', '+
' PRAO.code as ''Code'', '+
' PRAO.value as ''Value'','+
' PRAO.value_type as ''ValueType'''+
' FROM PBMM..PRIOR_REQUEST_ACT_OBSERVATION PRAO  with (Nolock)'+
''+
' where PRAO.request_id = ATran.id'+
' and PRAO.activity_id = PRA.ID'+
' FOR XML PATH (''Observation''),TYPE '+
' )'+
' '+
' FROM PBMM..PRIOR_REQUEST_ACTIVITY PRA with (Nolock)'+
' where PRA.prior_request_id = ATran.id'+
' FOR XML PATH (''Activity''),TYPE'+
' )'+
''+
' FOR XML PATH(''Authorization''),Type'+
' )'+
' FOR XML PATH(''Prior.Request''),Type'+
' )'+
' from PBMM..AUTHORIZATION_TRANSACTION ATran with(nolock)'+
+ ' INNER JOIN PBMM..PRIOR_REQUEST PR WITH (Nolock) ON ATran.id = pr.id'
+ ' INNER JOIN PBMM..[Payer] Pay WITH (nolock) ON pay.id = ATran.payer_id'
+ ' INNER JOIN PBMM..PRIOR_REQUEST_HEADER PRH WITH (Nolock) ON PRH.prior_request_id = PR.id'
+ ' INNER JOIN PBMM..[PROVIDER] P WITH (nolock) ON P.id = ATran.provider_id'
+ ' INNER JOIN PBMM..[MEMBER] M WITH (nolock) ON M.id = ATran.member_id'
+ ' LEFT JOIN PBMM..POST_OFFICE_COMM POC WITH (Nolock) ON POC.Trans_ID = ATran.id'
+ ' LEFT JOIN PBMM..PRIOR_REQUEST_RESUBMISSION PRR WITH (Nolock) ON PRR.request_id = PR.id' + 
' where ATran.id = ' + cast(@TransactionID as varchar(1000))+
' FOR XML PATH(''''),ROOT(''Cancel'')'



declare @filename varchar(1000)
set @filename = ''
set @filename = 'c:\tmp\PR_Cancel\CR_' + cast(@transactionID as varchar(1000))+ '.XML'
--set @filename = 'c:\PayerIntegration\Cancel\NonProcessed\CR_' + cast(@transactionID as varchar(1000))+ '.XML'
--set @filename = 'C:\tmp\Aafia\PR_Cancel\CR_' + cast(@transactionID as varchar(1000))+ '.XML'

--select @SQL
--sqlcmd -S @server -U loginid -P @LoginID -d @DbName -Q @SQL -o @filename
--set @sql = 'select getdate()'
declare @BCP1 varchar(8000) 
 
set @bcp1 = 'bcp "'+@SQL+'" QUERYOUT "'+@filename+'" -C -c -r -T -t,'
--'bcp '+ '" select getdate ()"' + ' QUERYOUT "C:\tmp\FILE_'+Cast(12 AS Varchar)+'.CSV" -C -c -T'
--select @BCP1 
--' bcp "'+ @sql  +'" queryout "' + @filename +'" -C -c -T  '
--	select @BCP1
EXEC xp_cmdshell @BCP1
   ----------------------------------------------------

   update top (1) PRIOR_REQUEST
	set download_canceled_batch_id = 'Picked'
	where id = @TransactionID
	Print 'Transaction Status updated successfully'  
   
    FETCH NEXT FROM Translist INTO @TransactionID

END
CLOSE TransList
DEALLOCATE TransList


