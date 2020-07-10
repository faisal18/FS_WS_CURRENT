Declare @TranCount int
Set @TranCount = 1000

declare @Payerid table(Payers nvarchar(100))
insert @Payerid(Payers) values (1),(3),(4),(7),(54),(1247),(1285),(1333),(1325),(1350),(1364)

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
and PR.download_batch_id in (null, '0')
and at.state_id IN (3,4,8)
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
set @SQL = ' '
+ ' select '
+ ' ATran.id as ''Id'''
+ ' ,pay.bucket_mapping_id as ''PayerId'''
+ ' ,(select REGION from PBMM..provider x with (nolock) where x.id = ATran.provider_id) as ''Region'''
+ ' ,''0'' as BatchId'
+ ' ,(CASE when PRH.xml_file_name is not null then PRH.xml_file_name when PRH.xml_file_name is null then POC.prior_request_file_name END) as  ''FileName'' '
+ ' ,'
+ ' ('
+ ' select '
+ ' (select PRH.sender_ID as ''SenderID'''
+ ' , PRH.receiver_ID as ''ReceiverID'''
+ ' , Convert(varchar(10),PRH.transaction_date,103) +'' ''+ Convert(varchar(5),PRH.transaction_date,8)  as ''TransactionDate'''
+ ' , PRH.record_count as ''RecordCount'''
+ ' , PRH.disposition_flag as ''DispositionFlag'' '
+ ' for XML path(''Header''),type ) '
+ ' ,('
+ ' select '
+ ' pr.[type] as ''Type'''
+ ' , ATran.request_id as ''ID'''
+ '  , PR.member_id as ''MemberID'''
+ ' , (case when Pay.license_no is null then pay.dubai_license_no when Pay.license_no = '''' then Pay.dubai_license_no ELSE Pay.license_no END ) as ''PayerID'''
+ ' , pr.emirates_id as ''EmiratesIDNumber'''
+ ' , ''PBM_'' + cast(ATran.id as varchar(1000) )+ ''_'' + cast(ATran.PCN_code as varchar(1000)) as ''IDPayer'''
+ ' ,  Convert(varchar(10),pr.date_ordered,103) as ''DateOrdered'''
+ ' , (select PRE.facility_ID as FacilityID,PRE.[type] as ''Type'' from PBMM..PRIOR_REQUEST_ENCOUNTER PRE  with (Nolock) where PRE.prior_Request_id = pr.id  	for xml path(''Encounter''), type   )'
+ ' , (select PRD.[type] as ''Type'',(case when PRD.mappedICD10_code is not null then PRD.mappedICD10_code ELSE PRD.code END ) as Code from PBMM..PRIOR_REQUEST_DIAGNOSIS PRD  with (Nolock) where PRD.prior_Request_id = pr.id  	for xml path(''Diagnosis''),  type   )'
+ ' ,(select ID as ID,Convert(varchar(10),start_date,103) +'' ''+ Convert(varchar(5),start_date,8) as ''Start'',type as ''Type'',code as Code,(case when P.Region = 2 THEN (Convert(decimal(10,2),PRA.pbm_quantity)) when P.Region != 2 THEN (Convert(decimal(10,2),PRA.quantity)) END) as ''Quantity'',Convert(decimal(10,2),net) as ''Net'',Clinician '
+ ','
+ ' ('
+ ' select [type] as ''Type'',[code] as Code,[value] as Value,[value_type] as ValueType from PBMM..PRIOR_REQUEST_ACT_OBSERVATION PRAO  with (Nolock) where PRAO.request_id = PR.id  and PRA.id = PRAO.Activity_ID  '
+ ' for xml path(''Observation''), Type   ) '
+ ' from PBMM..PRIOR_REQUEST_ACTIVITY PRA  with (Nolock) where PRA.prior_Request_id = PR.id  '
+ ' for xml path(''Activity''), type )'
+ ' ,'
+ ' PRR.type as ''ResubType'', PRR.comment as ''ResubComment'', PRR.attachment as ''ResubAttach'' '
+ ' for xml path(''Authorization''),Type'
+ ' ) '
+ ' for xml path (''Prior.Request''),type   '
+ ' )'
+ ' FROM PBMM..AUTHORIZATION_TRANSACTION ATran with(nolock)'
+ ' INNER JOIN PBMM..PRIOR_REQUEST PR WITH (Nolock) ON ATran.id = pr.id'
+ ' INNER JOIN PBMM..[Payer] Pay WITH (nolock) ON pay.id = ATran.payer_id'
+ ' INNER JOIN PBMM..PRIOR_REQUEST_HEADER PRH WITH (Nolock) ON PRH.prior_request_id = PR.id'
+ ' INNER JOIN PBMM..[PROVIDER] P WITH (nolock) ON P.id = ATran.provider_id'
+ ' INNER JOIN PBMM..[MEMBER] M WITH (nolock) ON M.id = ATran.member_id'
+ ' LEFT JOIN PBMM..POST_OFFICE_COMM POC WITH (Nolock) ON POC.Trans_ID = ATran.id'
+ ' LEFT JOIN PBMM..PRIOR_REQUEST_RESUBMISSION PRR WITH (Nolock) ON PRR.request_id = PR.id '
+ ' where ATran.id = ' + cast(@TransactionID as varchar(1000))
+ ' for xml path (''Request''),type '

declare @filename varchar(1000)
set @filename = ''
set @filename = 'c:\tmp\PR\PR_' + cast(@transactionID as varchar(1000))+ '.XML'
--set @filename = 'c:\PayerIntegration\PriorRequest\NonProcessed\PR_' + cast(@transactionID as varchar(1000))+ '.XML'
--set @filename = 'C:\tmp\Aafia\PR\PR_' + cast(@transactionID as varchar(1000))+ '.XML'
--select @SQL
--sqlcmd -S @server -U loginid -P @LoginID -d @DbName -Q @SQL -o @filename
--set @sql = 'select getdate()'
declare @BCP1 varchar(8000) 
 
set @bcp1 = 'bcp "'+@SQL+'" QUERYOUT "'+@filename+'" -C -c -T -t,'



--'bcp '+ '" select getdate ()"' + ' QUERYOUT "C:\tmp\FILE_'+Cast(12 AS Varchar)+'.CSV" -C -c -T'
--select @BCP1 
--' bcp "'+ @sql  +'" queryout "' + @filename +'" -C -c -T  '
--	select @BCP1
EXEC xp_cmdshell @BCP1
   ----------------------------------------------------

    update top (1) PRIOR_REQUEST
	set download_batch_id = 'Picked'
	where id = @TransactionID
	Print 'Transaction Status updated successfully' 
   
    FETCH NEXT FROM Translist INTO @TransactionID

END
CLOSE TransList
DEALLOCATE TransList

