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



Print 'Transaction Status updated successfully'   

declare @SQL varchar(max)
set @SQL = ' SELECT '+
' AT.id as ''Id'','+
' at.payer_id as ''PayerId'','+
' prov.REGION  as ''Region'','+
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
'				(CASE when Prov.Region = 2 then Convert(decimal(9,2),(PAA.drug_quantity * (select DCR.granual_units from PBMM..DRUG_CODE_REF DCR with (nolock) where DCR.id = PAA.drug_id))) when Prov.Region != 2 then Convert(decimal(9,2),(PAA.drug_quantity)) END) as ''Quantity'','+
'				Convert(decimal(9,2),PAA.drug_net) as ''Net'','+
'				Convert(decimal(9,2),PAA.drug_list_price) as ''List'','+
'				Convert(decimal(9,2),PAA.patient_share) as ''PatientShare'','+
'				Convert(decimal(9,2),PAA.payment_amount) as ''PaymentAmount'','+
'				PAA.denial_code as ''DenialCode''  '+


'				,(select ''Text'' AS ''Type'',''Requested_Net'' AS ''Code'',Convert(decimal(9,2),PAA.drug_gross) AS ''Value'',''Requested_Net'' AS ''ValueType'' FOR XML PATH (''Observation''),TYPE), '+
' 					(Select ''Text'' AS ''Type'',''MemberNo'' AS ''Code'', (select member_no from PBMM..MEMBER MEM with (nolock) where MEM.id = AT.member_id) AS ''Value'', ''MemberNo'' AS ''ValueType'' FOR XML PATH (''Observation''),TYPE), '+
' 					(Select ''Text'' AS ''Type'',''MiMemberNo'' AS ''Code'',(select MI_memberNo from PBMM..MEMBER MEM with (nolock) where MEM.id = AT.member_id) AS ''Value'', ''MiMemberNo'' AS ''ValueType'' FOR XML PAtH(''Observation''),TYPE) '+
' 					,(CASE WHEN PAA.denial_code IS NOT NULL THEN '+
'                             (SELECT '+
'                                 (SELECT ''Text'' AS ''Type'', ''PBM_Rejection_Code'' AS ''Code'', PAAD.denial_code AS ''Value'', ''PBM_Rejection_Code'' AS ''ValueType'' FOR XML PATH(''Observation''),TYPE), '+
'                                 (SELECT ''Text'' AS ''Type'', ''Comments'' AS ''Code'', REPLACE(REPLACE(PAAD.comment, CHAR(13), ''''), CHAR(10), '''') AS ''Value'', ''Comments'' AS ''ValueType'' FOR XML PATH (''Observation''),TYPE) '+
'                             FROM PBMM..PRIOR_AUTH_ACTIVITY_DENIAL PAAD WITH (nolock) '+
'                             WHERE PAAD.activity_id = PAA.id AND PAAD.authorization_id = PAA.authorization_id FOR XML PATH (''''),TYPE) '+
'                     END) ' +


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
' INNER JOIN PBMM..provider prov with (nolock) ON prov.id = AT.provider_id ' +
' LEFT JOIN PBMM..POST_OFFICE_COMM POC with (nolock) ON POC.trans_id = at.id'+
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

   update top(1) PRIOR_AUTHORIZATION
	set download_batch_id = 'Picked'
	where id = @TransactionID

    FETCH NEXT FROM Translist INTO @TransactionID

END
CLOSE TransList
DEALLOCATE TransList

