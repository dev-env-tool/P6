<Query Kind="Expression" />

using System;
using System.Globalization;
using System.Threading;

//--------------------------------------------------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------------------------------------
//-----Non nullable Variables for User choices to be nullable next----------------------------------------------------------

		public int NonNullableProductIdToInt = 0;
		public int NonNullableVersionIdToInt = 0;
		
//--------------------------------------------------------------------------------------------------------------------------
//--------------------------------------------------------------------------------------------------------------------------
//-----Nullable Variables for User choices which will be used for parameters in IQueriables---------------------------------
	
		public int ?ProductIdToInt = null;
		public int ?VersionIdToInt = null;
		public int ?IsSolvedToInt = null;
		public DateOnly  ?FilterTicketStartDate = null;
		public DateOnly  ?FilterTicketEndDate = null; //new ();
		public string CharacterStringToFindInTicket = null;
		
//--------------------------------------------------------------------------------------------------------------------------	
//--------------------------------------------------------------------------------------------------------------------------
//------Culture Info and DateTimeStyle simple declaration. dd/MM/yyyy in user entry to be converted in yyyy/MM/dd-----------


		public class CultureInfo();
		public class DateTimeStyles;

//--------------------------------------------------------------------------------------------------------------------------	
//--------------------------------------------------------------------------------------------------------------------------
//------Individual menu strings to be easily reusable anywhere--------------------------------------------------------------

		public string QuestionIsSolved = "Souhaitez-vous sélectionner les tickets en fonction de leur statut : Veuillez taper '0' pour non résolu '1' pour résolu, puis appuyez sur 'Entrer'.";
		public string QuestionFilterTicketStartDate = "Pour sélectionner les tickets dont la date de création est ultérieure à une date spécifique veuillez renseigner la date au format AAAA, MM, JJ, puis appuyez sur 'Entrer':";
		public string QuestionFilterTicketEndDate = "Pour sélectionner les tickets dont la date de résolution est antérieure à une date spécifique veuillez renseigner la date au format AAAA, MM, JJ, puis appuyez sur 'Entrer':";
		public string QuestionProductId = "Pour sélectionner les tickets en fonction d'un logiciel concerné veuillez renseigner son numéro correspondant, puis appuyez sur 'Entrer':";
		public string QuestionVersionId = "Pour sélectionner les tickets en fonction de la version du logiciel concerné veuillez renseigner son numéro correspondant, puis appuyez sur 'Entrer':";
		public string QuestionDateStart = "Pour sélectionner les tickets en fonction de la date de création, veuillez entrer la date au format : JJ/MM/AAAA, puis appuyez sur 'Entrer':";
		public string QuestionDateEnd = "Pour sélectionner les tickets en fonction de la date de résolution, veuillez entrer la date au format : JJ/MM/AAAA, puis appuyez sur 'Entrer':";
		public string QuestionSearchBar = "Pour sélectionner les tickets en fonction d'une phrase ou d'un mot clé, veuillez saisir les informations puis appuyez sur 'Entrer':";

		
		public string FilterIgnore = "Pour ignorer ce filtre appuyez directement sur 'Entrer'.";
		
		public string LineJump = "\n";
		
		public string IgnoredFilter = "Vous avez choisi d'ignorer ce filtre";
		public string CatchedError = "Veuillez entrer un nombre entier correspondant aux choix proposés ci-dessous.";
		public string ConfirmedChoice = "Votre choix a été enregistré. Votre saisie est la suivante : ";
	
		public string ProductIdHeader = "Id du produit";
		public string ProductNameHeader = "Id du produit";
	
//--------------------------------------------------------------------------------------------------------------------------	
//--------------------------------------------------------------------------------------------------------------------------
//------Program is begun and drove from main--------------------------------------------------------------------------------	
	
	
		public void Main()
		{
			MainMenuForFilters();
			QueriesAndDump();
		}
			
//--------------------------------------------------------------------------------------------------------------------------	
//--------------------------------------------------------------------------------------------------------------------------
//------Main Menu for user inputs-------------------------------------------------------------------------------------------
			
		void MainMenuForFilters()
		{
			QuestionFilterIsSolved();
			QuestionFilterProductId();
			if(ProductIdToInt > 0)
			{
				QuestionFilterVersionIdByProductId(NonNullableProductIdToInt);
			}
			QuestionFilterStartDate();
			if(IsSolvedToInt == 1)
			{
				QuestionFilterEndDate();
			}
			QuestionFilterSearchBar();
			return;
		}
		
//--------------------------------------------------------------------------------------------------------------------------	
//--------------------------------------------------------------------------------------------------------------------------
//------Redirection to the right query depending on user's search parameters------------------------------------------------
		
		public void QueriesAndDump()
		{
			switch (IsSolvedToInt)
			{
				case 0:
				var Query1 = getAllFilteredUnsolvedTickets().ToList();
				DumpTickets(Query1, IsSolvedToInt);
				break;
				
				case 1:
				var Query2 = getAllFilteredSolvedTickets().ToList();
				DumpTickets(Query2, IsSolvedToInt);
				break;	
					
				case null:
				var Query3 = getAllFilteredTickets().ToList();
				DumpTickets(Query3, IsSolvedToInt);
				break;
			}	
		}
//--------------------------------------------------------------------------------------------------------------------------	
//--------------------------------------------------------------------------------------------------------------------------
//------Dump tickets--------------------------------------------------------------------------------------------------------

		public void DumpTickets(List<Tickets> Tickets, int ?IsSolvedToInt)
		{
			int maxProductId = Tickets.Count();
			int maxYTableDimension = maxProductId + 1;
			int maxXTableDimension = 4;
			if (IsSolvedToInt == null || IsSolvedToInt == 1)
			{
				maxXTableDimension = 6;
			}
			string [,] ProductTableForDisplay = new string [maxYTableDimension,maxXTableDimension];
			ProductTableForDisplay[0,0] = "Numéro du ticket";
			ProductTableForDisplay[0,1] = "Statut du ticket";
			ProductTableForDisplay[0,2] = "Description du ticket";
			ProductTableForDisplay[0,3] = "Date de création du ticket";
			if (IsSolvedToInt == null || IsSolvedToInt == 1)
			{
				ProductTableForDisplay[0,4] = "Date de résolution du ticket";
				ProductTableForDisplay[0,5] = "Description de la résolution du ticket";
			}
			int i = 1;
			foreach (var ticket in Tickets)
			{		
				ProductTableForDisplay[i,0] = Convert.ToString(ticket.TicketId);
				IQueryable<bool> statusQuery = from ticketStatus in TicketStatuses .Where(ts => ticket.TicketStatusId == ts.TicketStatusId) select(ticketStatus.TicketStatus);
				bool status = statusQuery.First();
				if (status == false)
				{
					ProductTableForDisplay[i,1] = "En-cours";
				}
				else
				{
					ProductTableForDisplay[i,1] = "Résolu";
				}
				ProductTableForDisplay[i,2] = ticket.TicketDescription;
				ProductTableForDisplay[i,3] = Convert.ToString(ticket.TicketDateStart);
				if (IsSolvedToInt == null || IsSolvedToInt == 1)
				{
					ProductTableForDisplay[i,4] = Convert.ToString(ticket.TicketDateEnd);
					ProductTableForDisplay[i,5] = ticket.TicketFixDescription;
				}
				i++;
			}
			ProductTableForDisplay.Dump();
		}
	
//--------------------------------------------------------------------------------------------------------------------------	
//--------------------------------------------------------------------------------------------------------------------------
//------IQueries done at compile time---------------------------------------------------------------------------------------
	
		IQueryable<Tickets> getAllFilteredUnsolvedTickets()
		{
		 		return getAllUnsolvedTicketsFilteredByDatesAndProductAndVersionAndCharacterString
				(FilterTicketStartDate,FilterTicketEndDate,ProductIdToInt,VersionIdToInt,CharacterStringToFindInTicket);
		}
	
	
		IQueryable<Tickets> getAllFilteredSolvedTickets()
		{
		 		return getAllSolvedTicketsFilteredByDatesAndProductAndVersionAndCharacterString
				(FilterTicketStartDate,FilterTicketEndDate,ProductIdToInt,VersionIdToInt,CharacterStringToFindInTicket);
		}
	
	
		IQueryable<Tickets> getAllFilteredTickets()
		{

				return getAllTicketsFilteredByDatesAndProductAndVersionAndCharacterString
				(FilterTicketStartDate,FilterTicketEndDate,ProductIdToInt,VersionIdToInt,CharacterStringToFindInTicket);
		}
	

//--------------------------------------------------------------------------------------------------------------------------	
//--------------------------------------------------------------------------------------------------------------------------
//------Questions : parts of the main menu----------------------------------------------------------------------------------	

	
		void QuestionFilterIsSolved()
		{
			Console.WriteLine(QuestionIsSolved);
			Console.WriteLine(FilterIgnore + LineJump); 
			try
			{
				string IsSolved = Util.ReadLine();
				if (IsSolved == "")
				{
					Console.WriteLine(ConfirmedChoice + IgnoredFilter + LineJump);
					return;
				}
				
				IsSolvedToInt = Convert.ToInt32(IsSolved);
				if (IsSolvedToInt == 0 || IsSolvedToInt == 1)
				{
					Console.WriteLine(ConfirmedChoice + IsSolved + LineJump);
					return;
				}
	
			}
			catch
			{
				Console.WriteLine(CatchedError);
				QuestionFilterIsSolved();
			}
		}
	
	
		void QuestionFilterProductId()
		{
			Console.WriteLine(QuestionProductId);
			Console.WriteLine(FilterIgnore);
			var productsList = getAllProducts().ToList();
			var productIdList = new List <int>();
			productIdList = getAllProductIds().ToList();
			int maxProductId = productIdList.Count();
			int maxYTableDimension = maxProductId + 1;
			string [,] ProductTableForDisplay = new string [maxYTableDimension,2];
			ProductTableForDisplay[0,0] = "Numéro du produit";
			ProductTableForDisplay[0,1] = "Nom du produit";
			int i = 1;
			foreach (var product in productsList)
			{		
				ProductTableForDisplay[i,0] = Convert.ToString(product.ProductId);
				ProductTableForDisplay[i,1] = product.ProductName;
				i++;
			}
			ProductTableForDisplay.Dump();
			try
			{
				string ProductIdGivenByClient = Util.ReadLine();
				if (ProductIdGivenByClient == "")
				{
					Console.WriteLine(ConfirmedChoice + IgnoredFilter + LineJump);
					return;
				}
				
				NonNullableProductIdToInt = Convert.ToInt32(ProductIdGivenByClient);
				
				if (productIdList.Contains(NonNullableProductIdToInt))
				{
					Console.WriteLine(ConfirmedChoice + NonNullableProductIdToInt);
					ProductIdToInt = NonNullableProductIdToInt;
					return;
				}
				else
				{
					Console.WriteLine(CatchedError);
					QuestionFilterProductId();
				}
	
			}
			catch 
			{
				Console.WriteLine(CatchedError);	
				QuestionFilterProductId();
			}
		}
		
		void QuestionFilterVersionIdByProductId(int NonNullableProductIdToInt)
		{
			Console.WriteLine(QuestionVersionId);
			Console.WriteLine(FilterIgnore);
			var versionsList = getAllVersionsByProductId(NonNullableProductIdToInt);
			var versionIdList = new List <int>();
			versionIdList = getAllVersionIdsByProductId(NonNullableProductIdToInt).ToList();
			int maxVersionId = versionIdList.Count();
			int maxYTableDimension = maxVersionId + 1;
			string [,] VersionTableForDisplay = new string [maxYTableDimension,2];
			VersionTableForDisplay[0,0] = "Numéro de la version";
			VersionTableForDisplay[0,1] = "Nom de la version";
			int i = 1;
			foreach (var version in versionsList)
			{		
				VersionTableForDisplay[i,0] = Convert.ToString(version.VersionId);
				VersionTableForDisplay[i,1] = version.VersionName;
				i++;
			}
			VersionTableForDisplay.Dump();
			
			try
			{
				string VersionIdGivenByClient = Util.ReadLine();
				if (VersionIdGivenByClient == "")
				{
					Console.WriteLine(ConfirmedChoice + IgnoredFilter + LineJump);
					return;
				}
				
				
				if (NonNullableProductIdToInt > 0)
				{
					NonNullableVersionIdToInt = Convert.ToInt32(VersionIdGivenByClient);
				
					if (versionIdList.Contains(NonNullableVersionIdToInt))
					{
						Console.WriteLine(ConfirmedChoice + VersionIdGivenByClient);
						return;
					}
					else
					{
						Console.WriteLine(CatchedError);
						QuestionFilterVersionIdByProductId(NonNullableProductIdToInt);
					}
				}
			}
			catch 
			
			{
				Console.WriteLine(CatchedError);	
				QuestionFilterVersionIdByProductId(NonNullableProductIdToInt);
			}
		}
	
		void QuestionFilterStartDate()
		{
			Console.WriteLine(QuestionDateStart);
			Console.WriteLine(FilterIgnore);

			
			try
			{
				string StartDateGivenByClient = Util.ReadLine();
				if (StartDateGivenByClient == "")
				{
					Console.WriteLine(ConfirmedChoice + IgnoredFilter + LineJump);
					return;
				}
				else
				{
					FilterTicketStartDate = new ();
					FilterTicketStartDate = DateOnly.ParseExact(StartDateGivenByClient, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
					Console.WriteLine(ConfirmedChoice + FilterTicketStartDate);
					return;
				}
			}
			catch 
			{
				Console.WriteLine(CatchedError);	
				QuestionFilterStartDate();
			}
		}
	

		
		void QuestionFilterEndDate()
		{
			Console.WriteLine(QuestionDateEnd);
			Console.WriteLine(FilterIgnore);

			
			try
			{
				string EndDateGivenByClient = Util.ReadLine();
				if (EndDateGivenByClient == "")
				{
					Console.WriteLine(ConfirmedChoice + IgnoredFilter + LineJump);
					return;
				}
				else
				{
				 	FilterTicketEndDate= new ();
					FilterTicketEndDate = DateOnly.ParseExact(EndDateGivenByClient, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
					Console.WriteLine(ConfirmedChoice + FilterTicketEndDate);
					return;
				}
			}
			catch 
			{
				Console.WriteLine(CatchedError);	
				QuestionFilterStartDate();
			}
		}
		
		
		void QuestionFilterSearchBar()
		{
			Console.WriteLine(QuestionSearchBar);
			Console.WriteLine(FilterIgnore);

			
			try
			{
				string SearchStringGivenByClient = Util.ReadLine();
				if (SearchStringGivenByClient == "")
				{
					Console.WriteLine(ConfirmedChoice + IgnoredFilter + LineJump);
					return;
				}
				else
				{
					Console.WriteLine(ConfirmedChoice + FilterTicketStartDate);
					return;
				}
			}
			catch 
			{
				Console.WriteLine(CatchedError);	
				QuestionFilterSearchBar();
			}
		}



//--------------------------------------------------------------------------------------------------------------------------	
//--------------------------------------------------------------------------------------------------------------------------
//------Intermediary IQueries used in some places (menu, queries)-----------------------------------------------------------

		
		public IQueryable<Products> getAllProducts()
		{
		
			var selectAllProducts = from product in Products .Where(p => (p.ProductId > 0)) select(product);
			return selectAllProducts;
		}
		
		public IQueryable<int> getAllProductIds()
		{
			var selectAllProductIds = (from product in Products .Where(p => (p.ProductId > 0))select(product.ProductId));
			return selectAllProductIds;
		}
		
		
		public IQueryable<Versions> getAllVersionsByProductId(int ProductId)
		{
			var selectAllProductVersionsByProductId = (from productVersion in ProductsVersions .Where(pv => (pv.ProductId == ProductId))select(productVersion.ProductVersionId)).ToList();
			var selectAllVersions = (from version in Versions .Where(v => selectAllProductVersionsByProductId.Contains(v.VersionId))select(version));
			return selectAllVersions;
		}
		
		public IQueryable<int> getAllVersionIdsByProductId(int ProductId)
		{
			var selectAllProductVersionsByProductId = (from productVersion in ProductsVersions .Where(pv => (pv.ProductId == ProductId))select(productVersion.ProductVersionId)).ToList();
			var selectAllVersionsIds = (from version in Versions .Where(v => selectAllProductVersionsByProductId.Contains(v.VersionId))select(version.VersionId));
			return selectAllVersionsIds;
		}
		
	
//--------------------------------------------------------------------------------------------------------------------------	
//--------------------------------------------------------------------------------------------------------------------------
//------Main IQueries to show final results to the user---------------------------------------------------------------------

	
		public IQueryable<Tickets> getAllTicketsFilteredByDatesAndProductAndVersionAndCharacterString
		(DateOnly? FilterTicketStartDate, DateOnly? FilterTicketEndDate, int? ProductId, int? VersionId, string CharacterStringToFindInTicket)
		{
			var getAllFilteredTicketsByDates = (from ticket in Tickets
			
													.Where
													(
														t => (t.TicketDateStart >= FilterTicketStartDate && (FilterTicketEndDate == null)) ||
														(t.TicketDateEnd <= FilterTicketEndDate && (FilterTicketStartDate == null)) ||
														((FilterTicketStartDate == null) && (FilterTicketEndDate == null)) ||
														(t.TicketDateStart >= FilterTicketStartDate && t.TicketDateEnd <= FilterTicketEndDate) 
													)
													select (ticket));
													
													
			if (ProductId !=null || VersionId !=null || CharacterStringToFindInTicket != null)
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
				    return getAllFilteredTicketsByDatesAndProductAndVersionAndCharacterString;
				}
				else
				{
					return	getAllFilteredTicketsByDatesAndProductAndVersion;	
				}																	 	
			}
								
			return getAllFilteredTicketsByDates;
		}
		
		
		
		
		
		public IQueryable<Tickets> getAllUnsolvedTicketsFilteredByDatesAndProductAndVersionAndCharacterString
		(DateOnly? FilterTicketStartDate, DateOnly? FilterTicketEndDate, int? ProductId, int? VersionId, string CharacterStringToFindInTicket)
		{
			var getAllFilteredTicketsByDatesAndUnsolvedStatus = (from ticket in Tickets
			
																.Where
																(
																	t => (t.TicketDateStart >= FilterTicketStartDate && (FilterTicketEndDate == null)) ||
																	(t.TicketDateEnd <= FilterTicketEndDate && (FilterTicketStartDate == null)) ||
																	((FilterTicketStartDate == null) && (FilterTicketEndDate == null)) ||
																	(t.TicketDateStart >= FilterTicketStartDate && t.TicketDateEnd <= FilterTicketEndDate) 
																)
																from ticketStatus in TicketStatuses
																.Where(ts => ticket.TicketStatusId == ts.TicketStatusId && (ts.TicketStatus == false))
																select (ticket));
													
													
			if (ProductId !=null || VersionId !=null || CharacterStringToFindInTicket != null)
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
															
				var getAllFilteredTicketsByDatesAndProductAndVersion = getAllFilteredTicketsByDatesAndUnsolvedStatus.Where(t => getAllFilteredProductRunningSolutionIds.Contains(t.RunningSolutionId));
				
				if(CharacterStringToFindInTicket != null)
				{
					var getAllFilteredTicketsByDatesAndProductAndVersionAndCharacterString = getAllFilteredTicketsByDatesAndProductAndVersion
																							.Where
																							(
																								t => t.TicketDescription.Contains(CharacterStringToFindInTicket) ||
																								t.TicketFixDescription.Contains(CharacterStringToFindInTicket)
																							);
				    return getAllFilteredTicketsByDatesAndProductAndVersionAndCharacterString;
				}
				else
				{
					return	getAllFilteredTicketsByDatesAndProductAndVersion;	
				}																	 	
			}
								
			return getAllFilteredTicketsByDatesAndUnsolvedStatus;
		}
		
		
		
		
		
		
		public IQueryable<Tickets> getAllSolvedTicketsFilteredByDatesAndProductAndVersionAndCharacterString
		(DateOnly? FilterTicketStartDate, DateOnly? FilterTicketEndDate, int? ProductId, int? VersionId, string CharacterStringToFindInTicket)
		{
			var getAllFilteredTicketsByDatesAndSolvedStatus =   (from ticket in Tickets
			
															    .Where
															    (
																    t => (t.TicketDateStart >= FilterTicketStartDate && (FilterTicketEndDate == null)) ||
																    (t.TicketDateEnd <= FilterTicketEndDate && (FilterTicketStartDate == null)) ||
																    ((FilterTicketStartDate == null) && (FilterTicketEndDate == null)) ||
																    (t.TicketDateStart >= FilterTicketStartDate && t.TicketDateEnd <= FilterTicketEndDate) 
															    )
																from ticketStatus in TicketStatuses
																.Where(ts => ticket.TicketStatusId == ts.TicketStatusId && (ts.TicketStatus == true))
																select (ticket));
													
													
			if (ProductId !=null || VersionId !=null || CharacterStringToFindInTicket != null)
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
															
				var getAllFilteredTicketsByDatesAndProductAndVersion = getAllFilteredTicketsByDatesAndSolvedStatus.Where(t => getAllFilteredProductRunningSolutionIds.Contains(t.RunningSolutionId));
				
				if(CharacterStringToFindInTicket != null)
				{
					var getAllFilteredTicketsByDatesAndProductAndVersionAndCharacterString = getAllFilteredTicketsByDatesAndProductAndVersion
																							.Where
																							(
																								t => t.TicketDescription.Contains(CharacterStringToFindInTicket) ||
																								t.TicketFixDescription.Contains(CharacterStringToFindInTicket)
																							);
				    return getAllFilteredTicketsByDatesAndProductAndVersionAndCharacterString;
				}
				else
				{
					return	getAllFilteredTicketsByDatesAndProductAndVersion;	
				}																	 	
			}
								
			return getAllFilteredTicketsByDatesAndSolvedStatus;
		}