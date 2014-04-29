
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
	<body bgcolor="#404040"> 		    <title>Gallerie</title>
 <script language="javascript" type="text/javascript">
function displayPics()
{
  var photos = document.getElementById('galerie_mini') ;
  // On récupère l'élément ayant pour id galerie_mini
  var liens = photos.getElementsByTagName('a') ;
  // On récupère dans une variable tous les liens contenu dans galerie_mini
  var big_photo = document.getElementById('big_pict') ;
  // Ici c'est l'élément ayant pour id big_pict qui est récupéré, c'est notre photo en taille normale

  var titre_photo = document.getElementById('photo').getElementsByTagName('dt')[0] ;
  // Et enfin le titre de la photo de taille normale
  // Une boucle parcourant l'ensemble des liens contenu dans galerie_mini
  for (var i = 0 ; i < liens.length ; ++i) {
    // Au clique sur ces liens 
    liens[i].onclick = function() {
      big_photo.src = this.href; // On change l'attribut src de l'image en le remplaçant par la valeur du lien
      big_photo.alt = this.title; // On change son titre
      titre_photo.firstChild.nodeValue = this.title; // On change le texte de titre de la photo
      return false; // Et pour finir on inhibe l'action réelle du lien
    };
  }
}

// Il ne reste plus qu'à appeler notre fonction au chargement de la page
window.onload = displayPics;
</script>

 	<div id="galerie">
 
  		  <dl id="photo">
    <dt>Menu</dt><dd><img id="big_pict" src="images/Gallerie/menu.jpg" height="500" width="500"/></dd>  	</dl>
    
  <ul id="galerie_mini">
  <a href="images/Gallerie/menu.jpg" title="Menu"><img src="images/Gallerie/menu.jpg" height="100" width="100"/></a><a href="images/Gallerie/menu.jpg" title="Menu"><img src="images/Gallerie/menu.jpg" height="100" width="100"/></a><a href="images/Gallerie/Capture d’écran 2014-03-09 à 18.17.05.png" title="Fond d'écran"><img src="images/Gallerie/Capture d’écran 2014-03-09 à 18.17.05.png" height="100" width="100"/></a><a href="images/Gallerie/logo.png" title="Logo"><img src="images/Gallerie/logo.png" height="100" width="100"/></a></ul></br></br>
  	</div>
</div>
	</body>
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

</html> 