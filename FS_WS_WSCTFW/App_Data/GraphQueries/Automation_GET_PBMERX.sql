USE FS_WS_WSCTFW    
;with
ERX_TOTAL as (  select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc) rn   
	from monitoring_transactioncount 
	where  ApplicationName = 'ERX_Total_Count'  ),
ERX_PROCESSED as (  select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc) rn   
from monitoring_transactioncount 
where  ApplicationName = 'ERX_Processed_Count'  ),
ERX_PAYER as (select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc ) rn 
   from monitoring_transactioncount    
   where  ApplicationName = 'ERX_Payer_Processed_Count'   ),
--ERX_PENDING as (  select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc) rn    
--	from monitoring_transactioncount    
--	where  ApplicationName = 'ERX_Pending_Count'   ) , 
PBMLink_Total_Count as (  select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc) rn    
	from monitoring_transactioncount    
	where  ApplicationName = 'PBMLink_Total_Count'   ) , 
PBMLink_Processed_Count as (  select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc) rn    
	from monitoring_transactioncount    
	where  ApplicationName = 'PBMLink_Processed_Count'   )  ,
PBMLink_PBM_Payer_Processed_Count as (  select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc) rn    
	from monitoring_transactioncount    
	where  ApplicationName = 'PBMLink_PBM_Payer_Processed_Count'   )  
--PBMLink_Pending_Count as (  select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc) rn    
--	from monitoring_transactioncount    
--	where  ApplicationName = 'PBMLink_Pending_Count'   )  

select top 1
ERX_TOTAL.CheckingTime as 'CheckingTime' ,
ERX_TOTAL.[Count] as 'ERX_TOTAL',
ERX_PROCESSED.[Count] as 'ERX_PROCESSED' ,   
ERX_PAYER.[Count] as 'ERX_PAYER' ,   
--ERX_PENDING.[Count] as 'ERX_PENDING',
PBMLink_Total_Count.[Count] as 'PBMLink_TOTAL',
PBMLink_Processed_Count.[Count] as 'PBMLink_PROCESSED',
PBMLink_PBM_Payer_Processed_Count.[Count] as 'PBMLink_PAYER'
--PBMLink_Pending_Count.[Count] as 'PBMLink_PENDING' 

from ERX_TOTAL    
full outer join ERX_PROCESSED on ERX_TOTAL.rn = ERX_PROCESSED.rn    
full outer join ERX_PAYER on ERX_TOTAL.rn = ERX_PAYER.rn
--full outer join ERX_PENDING on ERX_TOTAL.rn = ERX_PENDING.rn

full outer join PBMLink_Total_Count on ERX_TOTAL.rn = PBMLink_Total_Count.rn
full outer join PBMLink_Processed_Count on ERX_TOTAL.rn = PBMLink_Processed_Count.rn
full outer join PBMLink_PBM_Payer_Processed_Count on ERX_TOTAL.rn = PBMLink_PBM_Payer_Processed_Count.rn
--full outer join PBMLink_Pending_Count on ERX_TOTAL.rn = PBMLink_Pending_Count.rn
  