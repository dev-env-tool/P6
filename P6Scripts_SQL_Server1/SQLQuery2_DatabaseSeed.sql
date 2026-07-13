--Database test seed data isert--
---------------------------------------------------------------------------------------------------------------------


--insert into OperatingSystems (OperatingSystemName)
--values 
--('Linux'),
--('MacOS'),
--('Windows'),
--('Android'),
--('iOS'),
--('WindowsMobile');


---------------------------------------------------------------------------------------------------------------------


--insert into Products (ProductName)
--values 
--('Trader en Herbe'),
--('Maître des Investissements'),
--('Planificateur d’Entraînement'),
--('Planificateur d’Anxiété Sociale');


---------------------------------------------------------------------------------------------------------------------


--insert into Versions (VersionName)
--values 
--('1.0'),
--('1.1'),
--('1.2'),
--('1.3'),
--('2.0'),
--('2.1');


---------------------------------------------------------------------------------------------------------------------


--insert into Versions (VersionName)
--values 
--('1.0'),
--('1.1'),
--('1.2'),
--('1.3'),
--('2.0'),
--('2.1');


---------------------------------------------------------------------------------------------------------------------


--insert into ProductsVersions (ProductId,VersionId)
--values 
--('1','1'),
--('1','2'),
--('1','3'),
--('1','4'),
--('2','1'),
--('2','5'),
--('2','6'),
--('3','1'),
--('3','2'),
--('3','5'),
--('4','1'),
--('4','2');


---------------------------------------------------------------------------------------------------------------------

--insert into RunningSolutions(OperatingSystemId,ProductVersionId)
--values 
--('1','1'),
--('3','1'),
------------
--('1','2'),
--('2','2'),
--('3','2'),
------------
--('1','3'),
--('2','3'),
--('3','3'),
--('4','3'),
--('5','3'),
--('6','3'),
------------
--('2','4'),
--('3','4'),
--('4','4'),
--('5','4'),
------------
--('2','1'),
--('5','1'),
------------
--('2','5'),
--('4','5'),
--('5','5'),
------------
--('2','6'),
--('3','6'),
--('4','6'),
--('5','6'),
------------
------------
--('4','2'),
--('5','2'),
--('6','2'),
------------
--('3','5'),
------------
--('4','1');

---------------------------------------------------------------------------------------------------------------------


--insert into TicketStatuses(TicketStatus)
--values 
--('1'),
--('1'),
--('1'),
--('1'),
--('1'),
--('1'),
--('1'),
--('1'),
--('1'),
--('1'),
--('1'),
--('1'),
--('0'),
--('0'),
--('0'),
--('0'),
--('1'),
--('0'),
--('0'),
--('0'),
--('0'),
--('0'),
--('0'),
--('0'),
--('0');



---------------------------------------------------------------------------------------------------------------------


--insert into Tickets(RunningSolutionId,TicketStatusId,TicketDateStart,TicketDateEnd,TicketDescription,TicketFixDescription)
--values 
--('1','1','2026-06-01','2026-06-06',
--'Un utilisateur souhaite réaliser une saisie dans le formulaire du profil utilisateur.
--Quand il remplit les différents champs et qu’il valide sa saisie, la page du formulaire reste
--affichée. Aucun message d’erreur n’est présent dans la page, ni au niveau des champs, ni en
--bas du formulaire. Le client après plusieurs essais s’est rendu compte que le caractère « ‘ »
--était la cause de ce dysfonctionnement.',
--'Envoi d’une demande à l’équipe de développement pour
--débloquer le caractère « ‘ » dans les champs. On demande à ce que les messages d’erreurs
--liés aux caractères spéciaux soient bien créés.'),



--('20','2','2026-06-02','2026-06-09',
--'Un client souhaite consulter les détails d’une entreprise cotée. Il tombe sur une
--page avec l’erreur : service « CompanyService » not running.',
--'Envoi d’une demande à l’équipe de développement pour faire
--démarrer le service.'),



--('5','3','2026-06-04','2026-06-09',
--'Un client n’arrive pas à ouvrir le module « Statistiques » auquel il vient de
--souscrire.',
--'Le service commercial a transmis au client un nouveau fichier
--de licence ayant le nom de client et le n° de poste correct. Ainsi le serveur d’activation pourra
--reconnaître la session.'),



--('22','4','2026-06-05','2026-07-15',
--'Un client souhaite réaliser un export de données au format .Xml. Le bouton est
--bien présent dans l’interface mais il est grisé.',
--'Envoi d’une demande à l’équipe de développement pour que
--le bouton d’export de données au format .Xml soit déverrouillé dans l’interface.'),



--('5','5','2026-06-05','2026-06-05',
--'Le client indique que son logiciel se coupe au démarrage du logiciel, en début de
--journée. Le client précise qu’il n’éteint jamais son ordinateur en fin de journée.',
--'On lui demande d’essayer après redémarrage de son
--ordinateur. La panne disparaît. On lui précise qu’il ferme les programmes et éteigne son
--ordinateur tous les soirs'),



--('14','6','2026-06-06','2026-06-11',
--'Un client souhaite changer son mot de passe. On lui demande de s’authentifier
--avec un code temporaire à remplir, reçu par mail. Le code expire au bout de 30 secondes
--alors que le mail met une minute à arriver.',
--'On envoie une demande à l’équipe de développement de sorte
--que le temps de validité du code soit augmenté à 3 minutes.'),



--('26','7','2026-06-08','2026-06-22',
--'Après une mise à jour, une interface du module de configuration de la base de
--données s’affiche en anglais alors que la langue d’utilisation sélectionnée est le français.',
--'On demande au service client français d’effectuer la traduction
--du fichier dans l’outil interne, puis de le transmettre à l’équipe de développement. L’équipe de
--développement a généré une hotfix spécifique.'),





--('19','8','2026-06-09','2026-06-18',
--'La page du calendrier ne permet pas de remonter ou avancer de plus de 6 mois
--autour de la date du jour.',
--'On demande au service de développement de modifier les
--conditions d’affichage de l’interface pour remonter ou avancer de 2 ans autour de la date du
--jour. On fait également en sorte de conserver les données à 3 ans autour de la date du jour
--pour avoir une marge de sécurité.'),




--('14','9','2026-06-10','2026-06-15','Quand un client modifie le nom d’une simulation d’achat, l’ancien nom reste
--affiché dans l’interface de présentation générale. Cependant le nouveau nom apparaît
--correctement dans toutes les autres interfaces.',
--'Envoi d’une demande au service de développement pour que
--la page de présentation des simulations fasse apparaître les noms de simulations modifiés.'),




--('18','10','2026-06-10','2026-06-23',
--'Lorsque le client souhaite renseigner son adresse, l’interface lui indique de choisir
--parmi la liste des adresses, or, celle-ci n’y est pas présente et il est impossible de la valider
--manuellement.',
--'Envoi d’une demande au service client pour qu’ un nouveau
--bouton permette de choisir le type de saisie : manuelle ou semi-automatique.'),



--('5','11','2026-06-10','2026-07-02',
--'Une nouvelle fonctionnalité intègre dans le logiciel un système de réseau social.
--Un client indique que cela ralentit fortement l’utilisation de son ordinateur quand d’autres
--applications Web fonctionnent en même temps.',
--'Une demande d’amélioration est effectuée auprès du service
--développement. On pourrait ajouter un sélecteur qui active ou désactive l’affichage du fil
--d’actualité dans l’interface. Et on met également un filtre qui permet à l’utilisateur de choisir le
--taux de rafraîchissement avec 3 options : lent / normal / élevé.'),



--('23','12','2026-06-11','2026-06-18',
--'Le client voit apparaître le prix des actions en $ US alors que la sélection de la
--devise est sur € et inversement.',
--'Une demande est envoyée au service de développement pour
--que le sigle sur le bouton corresponde à la devise affichée.'),




--('27','13','2026-06-11','',
--'L’affichage du formulaire de saisie d’une session d’entraînement n’est pas
--« responsive ». La colonne est trop large et correspond au format pour écran d’ordinateur et
--non pas à celui d’un téléphone.',
--''),




--('6','14','2026-06-11','',
--'Un utilisateur indique qu’un ordre d’achat est exécuté un jour après la date
--sélectionnée par l’utilisateur.',
--''),




--('21','15','2026-06-15','',
--'Après une mise à jour, un utilisateur ne voit plus la notification « votre ordre de
--vente a été traité ». La notification apparaît bien lors de l’achat de titres. Quand il va dans
--l’interface des dernières transactions effectuées, les ventes sont correctement affichées.',
--''),



--('5','16','2026-06-20','',
--'Un utilisateur constate que sa solution ralentit et se bloque au moment d’envoyer
--un rendez-vous à un de ses contacts. Un message d’erreur Windows lui demande s’il souhaite
--attendre que le programme réponde ou bien fermer l’application.',
--''),



--('17','17','2026-06-22','2026-06-22',
--'Un utilisateur a fait une demande d’amélioration. Elle permet d’avoir une
--notification dès qu’un ou plusieurs messages non lus sont dans la boîte de réception.',
--'Cette amélioration est déjà apparue en version 1.1, la mise à
--jour a été effectuée par le client.'),




--('13','18','2026-06-23','',
--'La fonctionnalité de récupération des états financiers de l’entreprise cotée ne
--fonctionne pas pour l’entreprise Legrand, il n’y a pas de message d’erreur.',
--''),




--('23','19','2026-06-24',
--'',
--'Un client tente de supprimer une simulation d’achat, or cette dernière reste visible
--dans l’interface des dernières simulations réalisées.',
--''),




--('18','20','2026-06-24','',
--'Un utilisateur a créé un nouvel entraînement dans son calendrier via l’application
--iOS de son téléphone. L’évènement n’est pas synchronisé dans l’appli du système MacOS.',
--''),




--('29','21','2026-06-25','',
--'Un utilisateur indique que les notifications instantanées destinées à prévenir une
--crise d’anxiété ne se déclenchent pas alors que dans l’application il a choisi l’option « activer
--les notifications en cas de détection de période d’anxiété.',
--''),




--('8','22','2026-06-27','',
--'Un utilisateur demande à avoir plus d’informations sur les différents sigles
--notamment au niveau des différents ratios financiers. Il souhaiterait voir ces informations en
--laissant la souris au niveau du champ (overlay).',
--''),



--('20','23','2026-06-27','',
--'Un client indique que les analyses financières ne précisent pas le format des états
--financiers utilisés : UsGaaP / IFRS.',
--''),



--('28','24','2026-06-28','',
--'Un utilisateur tente de personnaliser la disposition des widgets (drag and drop)
--dans la page d’accueil, or la disposition génère des espaces vides, il faudrait pouvoir aussi
--gérer la taille des widgets.',
--''),




--('5','25','2026-06-29','',
--'Un utilisateur souhaite avoir la possibilité d’entrer manuellement son propre type
--d’exercice contre l’anxiété face au public (trac) en plus des choix proposés par la solution.',
--'');




select * from Products
select * from Versions
select * from ProductsVersions
select * from OperatingSystems
select * from RunningSolutions
select * from TicketStatuses
select * from Tickets