<Query Kind="Program">
  <Connection>
    <ID>25368b19-4a2b-49a4-a2d6-81303a025728</ID>
    <NamingServiceVersion>3</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>(localdb)\MSSQLLocalDB</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <UseMicrosoftDataSqlClient>true</UseMicrosoftDataSqlClient>
    <EncryptTraffic>true</EncryptTraffic>
    <DisplayName>Local</DisplayName>
    <Database>P6Referential</Database>
    <MapXmlToString>false</MapXmlToString>
    <DriverData>
      <SkipCertificateCheck>true</SkipCertificateCheck>
    </DriverData>
  </Connection>
</Query>


	public bool IsSolved = true;
	public DateOnly  FilterTicketStartDate = new (2026, 06, 10);
	public DateOnly  FilterTicketEndDate = new (2026, 06, 20);
	public int ProductId = 1;
	public int VersionId = 4;
	public string CharacterStringToFindInTicket = "client";

	void Main()
	{
		//(from ticket in Tickets
		//		join runningSolution in RunningSolutions on ticket.RunningSolutionId equals runningSolution.RunningSolutionId
		//		select new {runningSolution.RunningSolutionId}).ToList().Dump();

			
		IQueryable<Tickets> getAllTicketsWithoutFilter = getAllTickets(null,null,null,null,null,null);
		getAllTicketsWithoutFilter.Dump();
		
		IQueryable<int> getAllTicketIdsWithoutFilter = getAllTicketIds(null,null,null,null,null,null);
		getAllTicketIdsWithoutFilter.Dump();
			
	}
	
	
	public IQueryable<Tickets> getAllTicketsByDates(DateOnly? FilterTicketStartDate, DateOnly? FilterTicketEndDate)
	{

		var getAllFilteredTicketsByDates = (from ticket in Tickets
							.Where
							(
								t => t.TicketDateStart >= FilterTicketStartDate && t.TicketDateEnd <= FilterTicketEndDate && 
								(FilterTicketStartDate != null)||(FilterTicketStartDate == null) &&
								(FilterTicketEndDate != null)||(FilterTicketEndDate == null)
							)
							select (ticket));
									
		return getAllFilteredTicketsByDates;
		

	}
	
	
	public IQueryable<Tickets> getAllTickets(bool? IsSolved, DateOnly? FilterTicketStartDate, DateOnly? FilterTicketEndDate,
	int? ProductId, int? VersionId, string? CharacterStringToFindInTicket)
	{

		var getAllFilteredTicketsByStatusesAndDates = (from ticket in Tickets
													.Where
													(
														t => t.TicketDateStart >= FilterTicketStartDate && t.TicketDateEnd <= FilterTicketEndDate && 
														(FilterTicketStartDate != null)||(FilterTicketStartDate == null) &&
														(FilterTicketEndDate != null)||(FilterTicketEndDate == null)
													)
													from ticketStatus in TicketStatuses
													.Where(ts => ticket.TicketStatusId == ts.TicketStatusId && 
													((IsSolved != null && ts.TicketStatus == IsSolved)||(IsSolved == null)))
													select (ticket));
									
		if (ProductId != null || VersionId != null)
		{						
									
			var getAllFilteredProductVersionIds = (from productVersion in ProductsVersions
												  .Where
												  (
														pv => pv.ProductId == ProductId && pv.VersionId == VersionId &&
														(ProductId != null)||(ProductId == null) &&
														(VersionId != null)||(VersionId == null)
												  )
												  select (productVersion.ProductVersionId));
												  
				
			var getAllFilteredProductRunningSolutionIds = (from runningSolution in RunningSolutions
												          .Where
												          (
														  		rs => rs.ProductVersionId.Equals(getAllFilteredProductVersionIds)
														  )
												  		  select (runningSolution.RunningSolutionId));
														  
														  											  
				  
			var getAllFilteredTicketsByStatusesAndDatesAndRunningSolution = getAllFilteredTicketsByStatusesAndDates;														
																				
			//return	getAllFilteredTicketsByStatusesAndDatesAndRunningSolution.Where(t => t.RunningSolutionId.Equals(getAllFilteredProductRunningSolutionIds));		
			
			getAllFilteredTicketsByStatusesAndDatesAndRunningSolution = getAllFilteredTicketsByStatusesAndDatesAndRunningSolution.Select(t => t);
			
			return	getAllFilteredTicketsByStatusesAndDatesAndRunningSolution.Where(t => t.RunningSolutionId.Equals(getAllFilteredProductRunningSolutionIds));																		 	
		}																								 
									
		return getAllFilteredTicketsByStatusesAndDates;
		

	}
	
	
	public IQueryable<int> getAllTicketIds(bool? IsSolved, DateOnly? TicketStartDate, DateOnly? TicketEndDate, 
	int? ProductId, int? ProductVersionId, string? CharacterStringToFindInTicket)
	{
		var getAllTicketIds = (from ticket in Tickets select (ticket.TicketId));
		return getAllTicketIds;
	}
	
		

	
	
