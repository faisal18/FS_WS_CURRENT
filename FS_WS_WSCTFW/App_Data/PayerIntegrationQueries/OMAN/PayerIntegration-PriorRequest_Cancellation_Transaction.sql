Declare @TranCount int
Set @TranCount =500

declare @Payerid int
set @Payerid = 7 

declare @region int
set @region = 3

Declare @TransactionID int
Declare @Server varchar (100)
set @Server = '10.156.176.85'
Declare @DBName varchar(100)
set @DBName = 'PBMM'
declare @loginId varchar(100)
set @loginId = 'fsheikh' 
declare @Password varchar(100)
set @Password = 'Dell@100'

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
 at.payer_id = @Payerid
and 
PR.download_canceled_batch_id in (null, '0','log','NULL')
and 
at.state_id = 8
and at.created_dt  < DATEADD(HOUR, -1, GETDATE())
and 
pr.[type] = 'Authorization'
--and at.provider_id in (select id from [PROVIDER] where Region in (@region))
---------------------------------------------

open TransList 
FETCH NEXT FROM Translist INTO @TransactionID


WHILE @@FETCH_STATUS = 0
BEGIN 
    
    PRINT @TransactionID
   
   -------------------------------------------------

update top (1) PRIOR_REQUEST
set download_canceled_batch_id = 'Picked'
where id = @TransactionID

Print 'Transaction Status updated successfully'   
declare @SQL varchar(max)
set @SQL = ' ' +

' Select '+
' AT.id as ''Id'','+
' AT.payer_id as ''PayerId'','+
' prov.REGION  as ''Region'','+
' ''0'' as ''BatchId'','+
' poc.prior_request_file_name as ''FileName'','+
' ('+
' SELECT'+
' ('+
' Select '+
' PRH.sender_ID as ''SenderID'','+
' PRH.receiver_ID as ''ReceiverID'','+
' Convert(varchar(10),PRH.transaction_date,103) +'' ''+ Convert(varchar(5),PRH.transaction_date,8) as ''TransactionDate'','+
' PRH.record_count as ''RecordCount'','+
' PRH.disposition_flag as ''DispositionFlag'''+
' from PBMM..PRIOR_REQUEST_HEADER PRH with (nolock)'+
' where PRH.prior_request_id = PR2.id'+
' FOR XML PATH (''Header''),TYPE'+
' ),'+
' ('+
' Select'+
' ''Cancellation'' as ''Type'','+
' [AT].request_id as ''ID'','+
' (select MEM.member_no from PBMM..MEMBER MEM with(nolock) where MEM.id = AT.member_id) as ''MemberID'','+
' (Select PAY.license_no from PBMM..PAYER PAY with (nolock) where PAY.id = [AT].payer_id) as ''PayerID'','+
' pr.emirates_id as ''EmiratesIDNumber'','+
'  ''PBM_'' + cast(at.id as varchar(1000) )+ ''_'' + cast(at.PCN_code as varchar(1000)) as ''IDPayer''' + 
' , Convert(varchar(10),pr.date_ordered,103)  as ''DateOrdered'','+
' ('+
' Select '+
' PRE.facility_ID as ''FacilityID'','+
' PRE.type as ''Type'''+
' FROM PBMM..PRIOR_REQUEST_ENCOUNTER PRE  with (Nolock) '+
' where PRE.prior_request_id = AT.id'+
' FOR XML PATH (''Encounter''),TYPE '+
' ),'+
' ('+
' Select'+
' PRD.type as ''Type'','+
' PRD.code as ''Code'''+
' FROM PBMM..PRIOR_REQUEST_DIAGNOSIS PRD  with (Nolock)'+
''+
' where PRD.prior_request_id = AT.id'+
' FOR XML PATH (''Diagnosis''),TYPE '+
' ),'+
''+
' ('+
' Select PRA.activity_ID as ''Id'','+
' Convert(varchar(10),PRA.start_date,103) +'' ''+ Convert(varchar(5),PRA.start_date,8)as ''StartDate'', '+
' PRA.type as ''Type'', '+
' PRA.code as ''Code'','+
' (case when Prov.Region = 2 THEN (Convert(decimal(9,2),PRA.pbm_quantity)) when Prov.Region != 2 THEN (Convert(decimal(9,2),PRA.quantity)) END) as ''Quantity'','+
' Convert(decimal(9,2),PRA.net) as ''Net'', '+
' PRA.clinician as ''Clinician'','+
' ('+
' Select '+
' PRAO.type as ''Type'', '+
' PRAO.code as ''Code'', '+
' PRAO.value as ''Value'','+
' PRAO.value_type as ''ValueType'''+
' FROM PBMM..PRIOR_REQUEST_ACT_OBSERVATION PRAO  with (Nolock)'+
''+
' where PRAO.request_id = AT.id'+
' and PRAO.activity_id = PRA.ID'+
' FOR XML PATH (''Observation''),TYPE '+
' )'+
' '+
' FROM PBMM..PRIOR_REQUEST_ACTIVITY PRA with (Nolock)'+
' where PRA.prior_request_id = at.id'+
' FOR XML PATH (''Activity''),TYPE'+
' )'+
''+
' from PBMM..PRIOR_REQUEST PR with(nolock)'+
' where AT.id = PR.id'+
' FOR XML PATH(''Authorization''),Type'+
' )'+
' FOR XML PATH(''Prior.Request''),Type'+
' )'+
' from PBMM..AUTHORIZATION_TRANSACTION AT with(nolock)'+
' inner join PBMM..Prior_Request PR2 with(nolock) on PR2.id = AT.id'+
' inner join PBMM..provider prov with (nolock) on prov.id = AT.provider_id'+
' left join PBMM..POST_OFFICE_COMM POC  with (Nolock) on POC.Trans_ID = AT.id'+
' where AT.id = ' + cast(@TransactionID as varchar(1000))+
--' --and PR2.type = ''Cancellation'''+
' FOR XML PATH(''''),ROOT(''Cancel'')'



declare @filename varchar(1000)
set @filename = ''
set @filename = 'c:\tmp\PR_Cancel\CR_' + cast(@transactionID as varchar(1000))+ '.XML'

select @SQL
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

   
   
    FETCH NEXT FROM Translist INTO @TransactionID

END
CLOSE TransList
DEALLOCATE TransList


--Select top 100 *  
--from 


-- AUTHORIZATION_TRANSACTION at  with (Nolock) 


--inner join PRIOR_REQUEST PR  with (Nolock) on At.id = pr.id
--inner join PRIOR_REQUEST_HEADER PRH  with (Nolock) on PRH.prior_request_id = PR.id
--inner join PRIOR_REQUEST_ENCOUNTER PRE  with (Nolock) on PRE.Prior_Request_Id = PR.ID
--inner join PRIOR_REQUEST_DIAGNOSIS PRD  with (Nolock) on PRD.prior_request_id = PR.ID
--inner join PRIOR_REQUEST_ACTIVITY PRA  with (Nolock) on PRA.prior_Request_id = PR.ID
--inner join PRIOR_REQUEST_ACT_OBSERVATION PRAO  with (Nolock) on PRAO.request_id = PR.ID  and PRA.id = PRAO.Activity_ID
--left join PRIOR_REQUEST_RESUBMISSION PRR  with (Nolock) on PRR.request_id = PR.id

--where at.id = @TransactionID




--select top 100 * from POST_OFFICE_COMM




--------------------------------------------------------------------------------------------------------------------------
--select 
--at3.request_id as 'AuthorizationTransaction.id'
--,at3.payer_id as 'PayerID'
--,(select REGION from provider x with (nolock) where x.id = at3.provider_id) as 'Region'
--,'0' as batchid
--,poc.prior_request_file_name as  'filename' 
--,
--(



--select 

--(select PRH.sender_ID as 'SenderID'
--, PRH.receiver_ID as 'ReceiverID'
--, PRH.transaction_date as 'TransactionDate'
--, PRH.record_count as 'RecordCount'
--, PRH.disposition_flag as 'DispositionFlag' from PBMM..PRIOR_REQUEST_HEADER PRH  
--with (Nolock) where  PRH.prior_request_id = PR2.id  for XML path('Header'),type ) 

----, PRH.sender_ID as 'SenderID'
----, PRH.receiver_ID as 'ReceiverID'
----, PRH.transaction_date as 'TransactionDate'
----, PRH.record_count as 'RecordCount'
----, PRH.disposition_flag as 'DispositionFlag'
--,(
--select 
-- pr.[type] as 'Type'
-- , at.request_id as 'ID'
-- --,m.member_no as MemberID
-- , at.member_id as 'MemberID'
-- , pay.license_no as 'PayerID'
-- , pr.emirates_id as 'EmiratesIDNumber'
-- ,  pr.date_ordered as 'DateOrdered'
-- --, --ENCOUNTER--
----PRE.facility_ID as 'EncounterFacilityID', PRE.type as 'EncounterType'
--, (select PRE.facility_ID,PRE.[type] from PBMM..PRIOR_REQUEST_ENCOUNTER PRE  with (Nolock) where PRE.prior_Request_id = pr.id  	for xml path('Encounter'), type   )
--, (select PRD.[type],code from PBMM..PRIOR_REQUEST_DIAGNOSIS PRD  with (Nolock) where PRD.prior_Request_id = pr.id  	for xml path('Diagnosis'),  type   )



--,(select id,start_date,type,code,quantity,net,Clinician 
--,
--(
--select [type],[code],[value],[value_type] from PBMM..PRIOR_REQUEST_ACT_OBSERVATION PRAO  with (Nolock) where PRAO.request_id = PR.id  and PRA.id = PRAO.Activity_ID  
--for xml path(''), root('Observation'), Type   ) 
--from PBMM..PRIOR_REQUEST_ACTIVITY PRA  with (Nolock) where PRA.prior_Request_id = PR.id  
--for xml path('Activity'), type )

--,
--PRR.type as 'ResubType', PRR.comment as 'ResubComment', PRR.attachment as 'ResubAttach' --,* 

--from  
-- PBMM..AUTHORIZATION_TRANSACTION at  with (Nolock)  

--inner join PBMM..PRIOR_REQUEST PR  with (Nolock) on At.id = pr.id  
-- inner join PBMM..[PROVIDER] P with (nolock) on P.id = at.provider_id  
-- inner join PBMM..[MEMBER] M with (nolock) on M.id = at.member_id
-- inner join PBMM..[Payer] Pay with (nolock) on pay.id = at.payer_id 
--  left join PBMM..PRIOR_REQUEST_RESUBMISSION PRR  with (Nolock) on PRR.request_id = PR.id  
   
--	  where at.id = at2.id
--  --order by DiagnosisType
--for xml path('Authorization'),Type
--) 

--from  
-- PBMM..AUTHORIZATION_TRANSACTION at2  with (Nolock)  

--inner join PBMM..PRIOR_REQUEST PR2  with (Nolock) on at2.id = PR2.id
-- where at2.id = at3.id 
-- for xml path ('Prior.Request'),type   

 
--)
--from AUTHORIZATION_TRANSACTION at3 with(nolock) 
--  left join PBMM..POST_OFFICE_COMM POC  with (Nolock) on POC.Trans_ID = at3.id 
--where at3.id = 667567
--for xml path ('Request'),type 