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
 inner join PRIOR_AUTHORIZATION PA  with (Nolock) on At.id = PA.id
where 
-- at.payer_id in (select PAY2.id from PBMM..PAYER PAY2 with (nolock) where PAY2.tpa_dubai_code = 'TPA026')
 at.payer_id in (select PAY2.id from PBMM..PAYER PAY2 with(nolock) where bucket_mapping_id in (select Payers from @Payerid))
and PA.download_batch_id in (null, '0')
and at.state_id IN (3,4,8)
and at.created_dt  < DATEADD(HOUR, -1, GETDATE())
--order by at.created_dt desc
--and at.provider_id in (select id from [PROVIDER] where Region in (@region))
---------------------------------------------

open TransList 
FETCH NEXT FROM Translist INTO @TransactionID


WHILE @@FETCH_STATUS = 0
BEGIN 
    
    PRINT @TransactionID
   
   -------------------------------------------------



   

declare @SQL varchar(max)
set @SQL = ' SELECT '+
' AT.id as ''Id'','+
' PAY.bucket_mapping_id as ''PayerId'','+
' prov.REGION  as ''Region'','+
' ''0'' as ''BatchId'','+
' (case when PAH.xml_file_name IS NOT NULL then PAH.xml_file_name else POC.prior_auth_file_name end) as ''FileName'','+
''+
'(Select '+
'	(Select '+
'		PAY.license_no as ''SenderID'','+
'		PAH.receiver_id as ''ReceiverID'','+
'		Convert(varchar(10),PAH.transaction_date,103) +'' ''+ Convert(varchar(5),PAH.transaction_date,8) as ''TransactionDate'','+
'		PAH.record_count as ''RecordCount'','+
'		PAH.disposition_flag as ''DispositionFlag'''+
'		FOR XML PATH (''Header''),TYPE'+
'	),'+
'	(Select '+
 '  (  case ' +
 '  when pa.result = ''Accepted'' Then ''Yes''' +
 '  when pa.result = ''Rejected'' then ''No'' '+
  ' end) as ''Result'' '+
'		,	AT.request_id as ''ID'','+
'			''PBM_'' + cast(at.id as varchar(1000) )+ ''_'' + cast(at.PCN_code as varchar(1000)) as ''IDPayer'','+
'			Convert(varchar(10),PA.start_date,103) +'' ''+ Convert(varchar(5),PA.start_date,8) as ''Start'','+
'			Convert(varchar(10),PA.end_date,103) +'' ''+ Convert(varchar(5),PA.end_date,8) as ''End'','+

'			CAST((select ''MemberID: ('' + CAST(MEM2.MI_memberNo as nvarchar) +'')'' from PBMM..MEMBER MEM2 with(nolock) where MEM2.id = AT.member_id) as nvarchar(MAX)) +'+
'					CAST( (Select'+
'							('' Activity('' + CAST(PAAD2.activity_id as nvarchar) + '',Claim Ref ID: '' + CAST(PAA2.pbm_claim_id as nvarchar) + '') '' + CAST(PAAD2.comment as nvarchar(MAX)) + '' ('' + CAST(PAAD2.denial_code as nvarchar) + '')'' )'+
'							From PBMM..PRIOR_AUTH_ACTIVITY PAA2 with (nolock) '+
'							left join PBMM..PRIOR_AUTH_ACTIVITY_DENIAL PAAD2 with(nolock) '+
'							on PAAD2.authorization_id = AT.id '+
'							AND PAAD2.activity_id = PAA2.id'+
'							where PAA2.authorization_id = AT.id'+
'							for xml path(''''),TYPE) as nvarchar(MAX)) '+
'			 as ''Comments'','+

'			(Select '+
'				PAA.activity_id as ''ID'','+
'				PAA.type as ''Type'','+
'				PAA.drug_code as ''Code'','+
'				(CASE'+
'					WHEN Prov.Region = 2 THEN Convert(decimal(10,2),(select PRA.pbm_quantity from PBMM..PRIOR_REQUEST_ACTIVITY PRA with(nolock) where PRA.code = PAA.drug_code and PRA.prior_request_id = PAA.authorization_id and PRA.drug_id = PAA.drug_id))'+
'					WHEN Prov.Region = 3 AND PAY.bucket_mapping_id = 7 THEN Convert(decimal(10,2),(select PRA.pbm_quantity from PBMM..PRIOR_REQUEST_ACTIVITY PRA with(nolock) where PRA.code = PAA.drug_code and PRA.prior_request_id = PAA.authorization_id and PRA.drug_id = PAA.drug_id))'+
'					ELSE Convert(decimal(10,2),(PAA.drug_quantity))'+
'				END) AS ''Quantity'','+
'				Convert(decimal(20,2),PAA.drug_net) as ''Net'','+
'				Convert(decimal(20,2),PAA.drug_list_price) as ''List'','+
'				Convert(decimal(20,2),PAA.patient_share) as ''PatientShare'','+
'				Convert(decimal(20,2),PAA.payment_amount) as ''PaymentAmount'','+

'				((CASE '+
'							WHEN prov.license_no is null or prov.license_no = '''''+
'							THEN ''AUTH-005'''+
'							ELSE'+
'							(select DC.pbm_denial_code+Char(10)  as ''data()'' '+
'							from PBMM..DENIAL_CODE DC with (nolock) '+
'							INNER JOIN PBMM..PRIOR_AUTH_ACTIVITY_DENIAL PAAD2 with (nolock) '+
'							ON PAAD2.denial_code = DC.pbm_denial_code '+
'							AND PAAD2.activity_id = PAA.id '+
'							AND PAAD2.authorization_id = AT.id'+
'							for xml path(''''),TYPE '+
'							)'+
'				END))'+
'				as ''DenialCode''  '+


'				   ,(select ''Text'' AS ''Type'',''Requested_Net'' AS ''Code'',Convert(decimal(20,2),PAA.drug_gross) AS ''Value'',''Requested_Net'' AS ''ValueType'' FOR XML PATH (''Observation''),TYPE), '+
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
' INNER JOIN PBMM..PRIOR_AUTH_HEADER PAH with(NOLOCK) ON PAH.authorization_id = AT.id' +
' LEFT JOIN PBMM..POST_OFFICE_COMM POC with (nolock) ON POC.trans_id = at.id'+
' INNER JOIN PBMM..PAYER PAY with (nolock) on PAY.id = AT.payer_id'+
'  where at.id = ' + cast(@TransactionID as varchar(1000)) +
'  FOR XML PATH(''Authorization''), type '-- ROOT('''')'

declare @filename varchar(2000)
set @filename = ''
set @filename = 'c:\tmp\PA\PA_' + cast(@transactionID as varchar(1000))+ '.XML'
--set @filename = 'c:\PayerIntegration\Authorization\NonProcessed\PA_' + cast(@transactionID as varchar(1000))+ '.XML'
--set @filename = 'C:\tmp\Aafia\PA\PA_' + cast(@transactionID as varchar(1000))+ '.XML'
--select @SQL
--sqlcmd -S @server -U loginid -P @LoginID -d @DbName -Q @SQL -o @filename
--set @sql = 'select getdate()'
declare @BCP1 varchar(8000) 
--select @BCP1   
set @bcp1 = 'bcp "'+@SQL+'" QUERYOUT "'+@filename+'" -C -c -r -T -t,'
--'bcp '+ '" select getdate ()"' + ' QUERYOUT "C:\tmp\FILE_'+Cast(12 AS Varchar)+'.CSV" -C -c -T'
--select @BCP1 
--' bcp "'+ @sql  +'" queryout "' + @filename +'" -C -c -T  '
--	select @BCP1
EXEC xp_cmdshell @BCP1
   ----------------------------------------------------

    update top(1) PRIOR_AUTHORIZATION
	set download_batch_id = 'Picked'
	where id = @TransactionID
	Print 'Transaction Status updated successfully'

    FETCH NEXT FROM Translist INTO @TransactionID

END
CLOSE TransList
DEALLOCATE TransList

