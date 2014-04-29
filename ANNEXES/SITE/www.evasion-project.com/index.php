<div id="bloc-page">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="fr">
    <head>
       <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.14/themes/base/jquery-ui.css" type="text/css" media="all" />
       <link rel="stylesheet" href="http://static.jquery.com/ui/css/demo-docs-theme/ui.theme.css" type="text/css" media="all" />
 
       <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.5.1/jquery.min.js" type="text/javascript"></script>
       <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.14/jquery-ui.min.js" type="text/javascript"></script>
   
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <link href="Style.css" rel="stylesheet" type="text/css" /> 
    <style type="text/css">
    </style>
    </head>
	<body bgcolor="#404040"> 	 	<img src="images/logo.png" alt="Logo-adhs" width="300" >
	    <title>Accueil</title>
				
 	    	 	<div id="news">
		<h2> Progression</h2> <a href="Articles.php?id=4&type=Post"><div id="nouveaute" style="background-image: url(images/Articles/3d_wallpapers_3.jpg); no-repeat center center fixed         -webkit-background-size: cover;
        -moz-background-size: cover;
        -o-background-size: cover;
        background-size: cover;  height: 250px; width: 48%; border: 1px solid grey;"><h5><strong>Modérateur</strong></h5> 
				<h1><strong>Ajout de la 3d</h1></strong> </br>
			</div> <a href="Articles.php?id=3&type=Post"><div id="nouveaute" style="background-image: url(images/Articles/Sound-waves-twitter-backgrounds-sound-waves-twitter-themes.jpg); no-repeat center center fixed         -webkit-background-size: cover;
        -moz-background-size: cover;
        -o-background-size: cover;
        background-size: cover;  height: 250px; width: 48%; border: 1px solid grey;"><h5><strong>Modérateur</strong></h5> 
				<h1><strong>Ajout du son</h1></strong> </br>
			</div> 		</br> </br>
		<h2> Evènements</h2> <a href="Articles.php?id=5&type=Event"><div id="nouveaute" style="background-image: url(images/Events/pdf.png); no-repeat center center fixed         -webkit-background-size: cover;
        -moz-background-size: cover;
        -o-background-size: cover;
        background-size: cover; height: 250px; width: 48%; border: 1px solid grey;"><h5><strong>2014-03-11</strong></h5> </br>
				<h1><strong>Soutenance 1</strong></h1> </br></br>
			</div> <a href="Articles.php?id=2&type=Event"><div id="nouveaute" style="background-image: url(images/Events/start.png); no-repeat center center fixed         -webkit-background-size: cover;
        -moz-background-size: cover;
        -o-background-size: cover;
        background-size: cover; height: 250px; width: 48%; border: 1px solid grey;"><h5><strong>2013-10-01</strong></h5> </br>
				<h1><strong>Démarrage du projet</strong></h1> </br></br>
			</div> </br></br></br>
 		 	</div>  
 			 <div id="menu" align="center"> 

<nav>
    <ul>
        <a href="index.php"><div id="premier"><img src="images/accueil.png"></div></a>
        <a href="Progression.php"><li>Progression</li></a>
        <a href="Evenements.php"><li>Evénements</li></a>
        <a href="Gallerie.php"><div id="dernier">Gallerie</div></a>
    </ul>
</nav> 

</div>
  	
    </div>
 <script language="javascript" type="text/javascript">
/* <![CDATA[ */
var moins="images/-.png";
var plus="images/+.png";
function cachemontre(id) {
  x=document.getElementById(id) ;
  if (x.style.display=="none") {x.style.display="block" ; }     else {x.style.display="none" ;}
}
function plusmoins(id) {
  x=document.getElementById(id) ;
  if (x.src==plus) {x.src=moins ;}
  else {x.src=plus ;}
}
/* ]]> */
</script>
 	<aside>
		Ce site présente l'évolution du projet Evasion, mené à son terme par l'équipe Les Professionnels. Nous exposons ici notre état d'esprit, nos avancées et partageons des screenshots régulièrement.
		<div id="q">
<table id="l0c0" border="0" cellspacing="0" cellpadding="3">
<tbody><tr><td>L'équipe </td><td>
<a onclick="plusmoins('l1c2');cachemontre('l1c3');" href="#l0c0">
<img id="l1c2" src="images/+.png" alt="" /></a>
</td><td>
<div id="l1c3" style="display: none;">
Louis Kédémos alias "El Parain"; </br>
Lenny Danino alias "Le Noob"; </br>
Khalis Chalabi; </br>
Anatole Moreau alias "Totonut";
</div>
</td></tr><tr><td>Téléchargement </td><td>
<a onclick="plusmoins('l2c2');cachemontre('l2c3');" href="#l0c0">
<img id="l2c2" src="images/+.png" alt="" /></a>
</td><td>
<div id="l2c3" style="display: none;">
<a href="https://github.com/Professionnels/PROJET"><u> Version 1 </u></a>
</div>
</td></tr></tbody></table>
		</div>

	</aside>    	<div id="barre_menu_recherche">
		<form action="index.php" method="post">
		<input type="text" name="q" /><input type="submit" value="Rechercher" />
			</form>
				</div>
	</body>

</html> 