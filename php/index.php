<?php 
namespace ahbsd\Zahlensysteme;

$gotPost = isset($_POST);

$numPostEntries = 0;

if ($gotPost) $numPostEntries = intval(count($_POST));

$a = 1976;
$aB = 40;
$b = '2N9C';
$bB = $aB;
$rechenweg = TRUE;
$useBack = 1;

if ($numPostEntries > 3) 
{
	$a = $_POST['a'];
	$aB = intval($_POST['aB']);
	
	if (isset($_POST['rechenweg'])) 
	{
		$rechenweg = TRUE;
	}
	else 
	{
		$rechenweg = FALSE;
	}
	
	$useBack = intval($_POST['useBack']);
	
	if ($useBack==0) {
		$b = $_POST['b'];
		$bB = intval($_POST['bB']);
	}
	else 
	{
		$bB = $aB;
	}
	
}


include_once './ahbsd.Zahlensysteme.php';



if (isset($_GET)) {
	if (isset($_GET['a'])) $a = $_GET['a'];
}
?>
<html>
<head>
<link href="default.css" rel="stylesheet" title="Default Style">
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<meta name="keywords" content="Zahlensystem, Zahlensysteme">
<title>Zahlensysteme</title>
<?php 
printf("<!-- gotPost: %1\$s -->\n", $gotPost);
echo "<!-- ";
print_r($_POST);
if ($numPostEntries>0)
{
	print_r($_POST);
}
else 
{
	printf("_POST enthält %1\%s Einträge", $numPostEntries);	
}
echo " -->\n";
?>
</head>
<body>
<h1>Zahlensystem-Umrechnung</h1>
<p>Zur Differenzierung, der ansonsten üblichen Darstellung von Zahlen einer
anderen Zahlenbasis, wie <span class="code">0b1010110</span>, <span class="code">
0xAFFE01</span> oder <span class="code">0c7472103</span>, wird hier ein anderes
Markierungssystem verwendet: <span class="code"><i>Basis</i>x<i>Zahl</i></span>.
</p>

<?php
	echo '<h2>10x' . $a . ' in Basis ' . $aB . '</h2>' ;
	$b36 = Base::Base10toBaseX($a, $aB, $rechenweg);
	printf("<p>10x%1\$s = %3\$sx%2\$s</p>\n", $a, $b36, $aB);

	if ($useBack==1) 
	{
		$b = $b36;
		$bB = $aB;
	}
	echo '<h2>' . $bB. 'x' . $b . ' in Basis 10</h2>';
	 	
	$b10 = Base::BaseXtoBase10($b, $bB, $rechenweg);
	printf("<p>%3\$sx%1\$s = 10x%2\$s</p>\n", $b, $b10, $bB);
?>
<hr/>
<form action="/zahlensysteme/" method="post" enctype="application/x-www-form-urlencoded" target="_self">
<table id="FORM">
<tr><th>&nbsp;</th><th>Wert</th></tr>
<tr><td>Wert a:</td><td><input type="number" length="20" name="a" value="<?php echo $a; ?>"></td></tr>
<tr><td>Ziel-Basis a:</td><td><input type="number" length="4" name="aB" value="<?php  echo $aB; ?>"></td></tr>
<tr><td>Zurück rechnen?</td><td><input name="useBack" type="radio" value="1" text="Ja"<?php if ($useBack==1) echo " checked"; ?>> Ja | <input type="radio" name="useBack" value="0" text="Nein"<?php if ($useBack==0) echo " checked"; ?>> Nein</td></tr>
<tr><td colspace="2"><input type="checkbox" name="rechenweg" value="1"<?php if ($rechenweg) echo " checked"; ?>>&nbsp;Rechenweg anzeigen?</td></tr>
<tr><td colspan="2"><input type="reset" text="Zurücksetzen"><input type="submit" text="Absenden"></td></tr>
</table>
</form>
</body>
</html>