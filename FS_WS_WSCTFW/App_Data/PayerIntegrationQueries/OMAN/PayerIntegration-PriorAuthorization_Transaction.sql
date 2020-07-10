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
 inner join PRIOR_AUTHORIZATION PA  with (Nolock) on At.id = PA.id
where 
 at.payer_id = @Payerid
and PA.download_batch_id in (null, '0')
and at.state_id IN (3,4,8)
and at.created_dt  < DATEADD(HOUR, -1, GETDATE())
--and at.provider_id in (select id from [PROVIDER] where Region in (@region))
---------------------------------------------

open TransList 
FETCH NEXT FROM Translist INTO @TransactionID


WHILE @@FETCH_STATUS = 0
BEGIN 
    
    PRINT @TransactionID
   
   -------------------------------------------------

update top(1) PRIOR_AUTHORIZATION
set download_batch_id = 'Picked'
where id = @TransactionID

Print 'Transaction Status updated successfully'   

declare @SQL varchar(max)
set @SQL = ' SELECT '+
' AT.id as ''Id'','+
' at.payer_id as ''PayerId'','+
' (select REGION from PBMM..provider prov with (nolock) where prov.id = AT.provider_id)  as ''Region'','+
' ''0'' as ''BatchId'','+
' POC.prior_auth_file_name as ''FileName'','+
--'--(select PAH2.xml_file_name from PRIOR_AUTH_HEADER PAH2 where PAH2.authorization_id = AT.id) as ''FileName'',--'+
''+
'(Select '+
'	(Select '+
'		PAH.sender_id as ''SenderID'','+
'		PAH.receiver_id as ''ReceiverID'','+
'		Convert(varchar(10),PAH.transaction_date,103) +'' ''+ Convert(varchar(5),PAH.transaction_date,8) as ''TransactionDate'','+
'		PAH.record_count as ''RecordCount'','+
'		PAH.disposition_flag as ''DispositionFlag'''+
'		FROM PBMM..PRIOR_AUTH_HEADER PAH with(NOLOCK)'+
'		where PAH.authorization_id = AT.id'+
'		FOR XML PATH (''Header''),TYPE'+
'	),'+
'	(Select '+
 '  (  case ' +
 '  when pa.result = ''Accepted'' Then ''Yes''' +
 '  when pa.result = ''Rejected'' then ''No'' '+
  ' end) as ''Result'' '+
'		,	AT.request_id as ''ID'','+
'			''PBM_'' + cast(at.id as varchar(1000) )+ ''_'' + cast(at.PCN_code as varchar(1000)) as ''IDPayer'','+
'			PA.denial_code as ''DenialCode'','+
'			Convert(varchar(10),PA.start_date,103) +'' ''+ Convert(varchar(5),PA.start_date,8) as ''Start'','+
'			Convert(varchar(10),PA.end_date,103) +'' ''+ Convert(varchar(5),PA.end_date,8) as ''End'','+
'			PA.comment as ''Comments'','+
'			(Select '+
'				PAA.activity_id as ''ID'','+
'				PAA.type as ''Type'','+
'				PAA.drug_code as ''Code'','+
'				Convert(decimal(9,2),PAA.drug_quantity) as ''Quantity'','+
'				Convert(decimal(9,2),PAA.drug_net) as ''Net'','+
'				Convert(decimal(9,2),PAA.drug_list_price) as ''List'','+
'				Convert(decimal(9,2),PAA.patient_share) as ''PatientShare'','+
'				Convert(decimal(9,2),PAA.payment_amount) as ''PaymentAmount'','+
'				PAA.denial_code as ''DenialCode''  '+

-- ' (select '+
-- '  ''Text'' as ''Type'','+
-- '  ''Requested_Net'' as ''Code'','+
-- ' Convert(decimal(9,2),PAA.drug_gross) as ''Value'','+
-- '  ''Requested_Net'' as ''ValueType'''+
-- '   from PBMM..PRIOR_AUTH_ACTIVITY PAA'+
-- '   where PAA.authorization_id = PA.id'+
-- '   FOR XML PATH (''Observation''),Type),'+

-- '  (case'+
-- '  when PAA.denial_code is not null'+
-- '  THEN(SELECT'+
-- '		(select '+
-- '		''Text'' as ''Type'','+
-- '		''PBM_Rejection_Code'' as ''Code'','+
-- '		 PAAD.denial_code as ''Value'','+
-- '		''PBM_Rejection_Code'' as ''ValueType'''+
-- '		from PBMM..PRIOR_AUTH_ACTIVITY_DENIAL PAAD with (nolock)'+
-- '		where PAAD.authorization_id = PA.id'+
-- '		FOR XML PATH (''Observation''),Type)'+
-- '		,(select '+
-- '		''Text'' as ''Type'','+
-- '		''Comments'' as ''Code'','+
-- '		REPLACE(REPLACE(PAAD.comment, CHAR(13), ''''), CHAR(10), '''') as ''Value'','+
-- '		''Comments'' as ''ValueType'''+
-- '		from PBMM..PRIOR_AUTH_ACTIVITY_DENIAL PAAD with (nolock)'+
-- '		where PAAD.authorization_id = PA.id'+
-- '		FOR XML PATH (''Observation''),Type)'+
-- '		FOR XML PATH (''''),TYPE'+
-- '	)'+
-- 'END)'+


'				FROM PBMM..PRIOR_AUTH_ACTIVITY PAA with (nolock)'+
'				where PAA.authorization_id = PA.id'+
'				FOR XML PATH (''Activity''),TYPE '+
'			)'+
'			FROM PBMM..PRIOR_AUTHORIZATION PA with (nolock) '+
'			where PA.id = AT.id'+
'			FOR XML PATH (''Authorization''),TYPE '+
'	)'+
' FOR XML PATH(''Prior.Authorization''),TYPE '+
' ) '+

' from PBMM..AUTHORIZATION_TRANSACTION [AT] '+
' INNER JOIN PBMM..POST_OFFICE_COMM POC with (nolock) ON POC.trans_id = at.id'+
'  where at.id = ' + cast(@TransactionID as varchar(1000)) +
'  FOR XML PATH(''Authorization''), type '-- ROOT('''')'

declare @filename varchar(2000)
set @filename = ''
set @filename = 'c:\tmp\PA\PA_' + cast(@transactionID as varchar(1000))+ '.XML'
--set @filename = 'c:\PayerIntegration\Authorization\NonProcessed\PA_' + cast(@transactionID as varchar(1000))+ '.XML'
select @SQL
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

    FETCH NEXT FROM Translist INTO @TransactionID

END
CLOSE TransList
DEALLOCATE TransList

-------------------------------------------------------------------------------------
--use PBMM

--select top 100 * 
--from AUTHORIZATION_TRANSACTION at 
--inner join [provider] P with (nolock) on p.id = at.provider_id
--left join POST_OFFICE_COMM POC with (nolock) on At.id = poc.trans_id
--inner  JOIN [PRIOR_AUTHORIZATION] PA   on  AT.id = PA.ID
--inner join PRIOR_AUTH_HEADER PAH on PA.ID = PAH.Authorization_id
--Inner JOIN PRIOR_AUTH_ACTIVITY PAA   on PAA.Authorization_ID = PA.ID 
--inner join PRIOR_AUTH_ACTIVITY_DENIAL PAAD on PAAD.authorization_id = PAA.AUthorization_ID and PAA.ID = PAAD.Activity_ID




--------------------------------------------------------------------------------------------------------------------------

