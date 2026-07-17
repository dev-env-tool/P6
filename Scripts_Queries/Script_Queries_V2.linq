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
	public int ProductId = 2;
	public int VersionId = 4;
	public string CharacterStringToFindInTicket = "client";

	public string QuestionIsSolved = "Souhaitez-vous sélectionner les tickets en fonction de leur statut : Veuillez taper '0' pour non résolu '1' pour résolu, puis appuyez sur 'Entrer'.";
	public string QuestionFilterTicketStartDate = "Pour sélectionner les tickets dont la date de création est ultérieure à une date spécifique veuillez renseigner la date au format AAAA, MM, JJ, puis appuyez sur 'Entrer':";
	public string QuestionFilterTicketEndDate = "Pour sélectionner les tickets dont la date de résolution est antérieure à une date spécifique veuillez renseigner la date au format AAAA, MM, JJ, puis appuyez sur 'Entrer':";
	public string QuestionProductId = "Pour sélectionner les tickets en fonction d'un logiciel concerné veuillez renseigner son numéro correspondant, puis appuyez sur 'Entrer':";
	public string QuestionVersionId = "Pour sélectionner les tickets en fonction de la version du logiciel concerné veuillez renseigner son numéro correspondant, puis appuyez sur 'Entrer':";
	
	
	string FilterIgnore = "Pour ignorer ce filtre appuyez directement sur 'Entrer'.";

	void Test()
	{
		Console.WriteLine(QuestionIsSolved);
		Console.WriteLine(FilterIgnore);
		
		Console.WriteLine(QuestionFilterTicketStartDate);
		Console.WriteLine(FilterIgnore);
		
		Console.WriteLine(QuestionFilterTicketEndDate);
		Console.WriteLine(FilterIgnore);
		
		
		Console.WriteLine(QuestionProductId);
		Console.WriteLine(FilterIgnore);
		
		Console.WriteLine(QuestionVersionId);
		Console.WriteLine(FilterIgnore);
		
		//Util.ReadLine();
	}
	





	void Main()
	{
		IQueryable<Tickets> getAllTicketsFilteredByStatusAndDates = getAllTicketsByStatusAndDatesAndProduct
		(FilterTicketStartDate,FilterTicketEndDate,ProductId,null);
		//getAllTicketsFilteredByStatusAndDates.Dump();		
		
		//IQueryable<Tickets> getAllTicketsFilteredByDates = getAllTicketsByDates(FilterTicketStartDate,null);
		//getAllTicketsFilteredByDates.Dump();
		Test();
	}
	
	
	public IQueryable<Tickets> getAllTicketsByDates(DateOnly? FilterTicketStartDate, DateOnly? FilterTicketEndDate)
	{

		var getAllFilteredTicketsByDates = (from ticket in Tickets
							.Where
							(
								t => (t.TicketDateStart >= FilterTicketStartDate && (FilterTicketEndDate == null)) ||
								(t.TicketDateEnd <= FilterTicketEndDate && (FilterTicketStartDate == null)) ||
								(t.TicketDateStart >= FilterTicketStartDate && t.TicketDateEnd <= FilterTicketEndDate) 
							)
							select (ticket));
									
		return getAllFilteredTicketsByDates;
		

	}
	
	
	public IQueryable<Tickets> getAllTicketsByStatusAndDatesAndProduct(DateOnly? FilterTicketStartDate, DateOnly? FilterTicketEndDate, 
	int? ProductId, int? VesrionId)
	{
		var getAllFilteredTicketsByDates = (from ticket in Tickets
		
												.Where
												(
													t => (t.TicketDateStart >= FilterTicketStartDate && (FilterTicketEndDate == null)) ||
													(t.TicketDateEnd <= FilterTicketEndDate && (FilterTicketStartDate == null)) ||
													(t.TicketDateStart >= FilterTicketStartDate && t.TicketDateEnd <= FilterTicketEndDate) 
												)
												select (ticket)).Dump();
												
												
	if (ProductId > 0 || VersionId > 0)
	{						
									
		var getAllFilteredProductVersionIds =   (from productVersion in ProductsVersions
												.Where
												(
													pv => (pv.ProductId == ProductId && pv.VersionId == VersionId) ||
													(pv.ProductId == ProductId) ||
													(pv.VersionId == VersionId)
													
													//(ProductId != null)||(ProductId == null) &&
													//(VersionId != null)||(VersionId == null)
												)
												select (productVersion.ProductVersionId)).Dump();
												  
				
		var getAllFilteredProductRunningSolutionIds =   (from runningSolution in RunningSolutions
												        .Where
												        (
															//rs => rs.ProductVersionId.Equals(getAllFilteredProductVersionIds)
															rs => getAllFilteredProductVersionIds.Contains(rs.ProductVersionId)
														)
												  		select (runningSolution.RunningSolutionId)).ToList().Dump();
													
		return	getAllFilteredTicketsByDates.Where(t => getAllFilteredProductRunningSolutionIds.Contains(t.RunningSolutionId)).Dump();																		 	
	}
													//join productVersion in ProductsVersions on ProductId equals productVersion.ProductId
													//join runningSolution in RunningSolutions on ticket.RunningSolutionId equals runningSolution.RunningSolutionId
															//from ticketStatus in TicketStatuses
															//.Where
															//(
															//	ts => ticket.TicketStatusId == ts.TicketStatusId && 
															//	((IsSolved != null && ts.TicketStatus == IsSolved)||(IsSolved == null))
															//)
															//.Where
													//(
													//	rs => rs.ProductVersionId == pv.ProductId
													//)
													
													//join runningSolution in RunningSolutions on productVersion.ProductVersionId equals runningSolution.ProductVersionId
													
													 
													
													//.Where
													//(
													//	pv => pv.ProductId == runningSolution.ProductVersionId
													//)
													
											
												  		  
													
		//		select new {runningSolution.RunningSolutionId}).ToList().Dump();
													
													
													
		//if (ProductId != null)
		//{						
									
		//	var getAllFilteredProductVersionIds = (from productVersion in ProductsVersions
		//										  .Where
		//										  (
		//												pv => pv.ProductId == ProductId
		//										  )
		//										  select (productVersion.ProductVersionId)).Dump();
												  
				
		//	var getAllFilteredProductRunningSolutionIds = (from runningSolution in RunningSolutions
		//										          .Where
		//										          (
		//														rs => getAllFilteredProductVersionIds.Contains(rs.ProductVersionId)
		//												  )
		//										  		  select (runningSolution.RunningSolutionId)).Dump();
														  
														  											  
				  
		//	var getAllFilteredTicketsByStatusesAndDatesAndRunningSolution = getAllFilteredTicketsByStatusAndDates;														
																				
		//	getAllFilteredTicketsByStatusesAndDatesAndRunningSolution = getAllFilteredTicketsByStatusesAndDatesAndRunningSolution.Select(t => t);
			
			
		//	getAllFilteredTicketsByStatusesAndDatesAndRunningSolution.Where(t => getAllFilteredProductRunningSolutionIds.Contains(t.RunningSolutionId));	
			
			
			
			
			
		//	return	getAllFilteredTicketsByStatusesAndDatesAndRunningSolution.Where(t => t.RunningSolutionId.Equals(getAllFilteredProductRunningSolutionIds));																		 	
		//}								
									
									
									
									
		return getAllFilteredTicketsByDates;
	}
	

	
		

	
	
