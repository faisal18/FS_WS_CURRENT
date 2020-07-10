USE FS_WS_WSCTFW    
;with
ERX_Payers_Pending as (  select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc) rn   
	from monitoring_transactioncount 
	where  ApplicationName = 'ERX_Payers_Pending_Count'  ),
	
ERX_NonPayers_Pending as (  select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc) rn   
from monitoring_transactioncount 
where  ApplicationName = 'ERX_NonPayers_Pending_Count'  ),

PBMLink_Payers_Pending_Count as (  select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc) rn    
	from monitoring_transactioncount    
	where  ApplicationName = 'PBMLink_Payers_Pending_Count'   )  ,

PBMLink_NonPayers_Pending_Count as (  select applicationname,checkingtime,[Count],row_number() over (order by checkingtime desc) rn    
	from monitoring_transactioncount    
	where  ApplicationName = 'PBMLink_NonPayers_Pending_Count'   )  


select top 1
ERX_Payers_Pending.CheckingTime as 'CheckingTime',
ERX_Payers_Pending.[Count] as 'ERX_Payers_Pending_Count' ,
ERX_NonPayers_Pending.[Count] as 'ERX_NonPayers_Pending_Count',
PBMLink_Payers_Pending_Count.[Count] as 'PBMLink_Payers_Pending_Count' ,   
PBMLink_NonPayers_Pending_Count.[Count] as 'PBMLink_NonPayers_Pending_Count' 


from ERX_Payers_Pending    
full outer join ERX_NonPayers_Pending on ERX_Payers_Pending.rn = ERX_NonPayers_Pending.rn  
full outer join PBMLink_Payers_Pending_Count on ERX_Payers_Pending.rn = PBMLink_Payers_Pending_Count.rn
full outer join PBMLink_NonPayers_Pending_Count on  ERX_Payers_Pending.rn = PBMLink_NonPayers_Pending_Count.rn
  