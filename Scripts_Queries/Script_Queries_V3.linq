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
		Test();
		IQueryable<Tickets> getAllTicketsFilteredByStatusAndDates = getAllTicketsFilteredByDatesAndProductAndVersionAndCharacterString
		(FilterTicketStartDate,FilterTicketEndDate,ProductId,null,CharacterStringToFindInTicket);
		//getAllTicketsFilteredByStatusAndDates.Dump();		
		
		//IQueryable<Tickets> getAllTicketsFilteredByDates = getAllTicketsByDates(FilterTicketStartDate,null);
		//getAllTicketsFilteredByDates.Dump();

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
	
	
	public IQueryable<Tickets> getAllTicketsFilteredByDatesAndProductAndVersionAndCharacterString
	(DateOnly? FilterTicketStartDate, DateOnly? FilterTicketEndDate, int? ProductId, int? VesrionId, string? CharacterStringToFindInTicket)
	{
		var getAllFilteredTicketsByDates = (from ticket in Tickets
		
												.Where
												(
													t => (t.TicketDateStart >= FilterTicketStartDate && (FilterTicketEndDate == null)) ||
													(t.TicketDateEnd <= FilterTicketEndDate && (FilterTicketStartDate == null)) ||
													(t.TicketDateStart >= FilterTicketStartDate && t.TicketDateEnd <= FilterTicketEndDate) 
												)
												select (ticket));
												
												
		if (ProductId > 0 || VersionId > 0 || CharacterStringToFindInTicket != null)
		{												
			var getAllFilteredProductVersionIds =   (from productVersion in ProductsVersions
													.Where
													(
														pv => (pv.ProductId == ProductId && pv.VersionId == VersionId) ||
														(pv.ProductId == ProductId) ||
														(pv.VersionId == VersionId)
													)
													select (productVersion.ProductVersionId));
													  
					
			var getAllFilteredProductRunningSolutionIds =   (from runningSolution in RunningSolutions
													        .Where
													        (
																rs => getAllFilteredProductVersionIds.Contains(rs.ProductVersionId)
															)
													  		select (runningSolution.RunningSolutionId)).ToList();
														
			var getAllFilteredTicketsByDatesAndProductAndVersion = getAllFilteredTicketsByDates.Where(t => getAllFilteredProductRunningSolutionIds.Contains(t.RunningSolutionId));
			
			if(CharacterStringToFindInTicket != null)
			{
				var getAllFilteredTicketsByDatesAndProductAndVersionAndCharacterString = getAllFilteredTicketsByDatesAndProductAndVersion
																						.Where
																						(
																							t => t.TicketDescription.Contains(CharacterStringToFindInTicket) ||
																							t.TicketFixDescription.Contains(CharacterStringToFindInTicket)
																						);
			    return getAllFilteredTicketsByDatesAndProductAndVersionAndCharacterString.Dump();
			}
			else
			{
				return	getAllFilteredTicketsByDatesAndProductAndVersion.Dump();	
			}																	 	
		}
							
		return getAllFilteredTicketsByDates;
	}
	

	
		

	
	
