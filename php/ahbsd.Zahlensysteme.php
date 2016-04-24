<?php
namespace ahbsd\Zahlensysteme
{
	/**
	 * Interface für grundlegende Funktionen, der Basis eines Zahlensystems.
	 * 
	 * @author A. Hermann
	 * @copy Open Source
	 * Zahlensysteme
	 *
	 * @version 1.0
	 *
	 */
  interface IBase
  {
    /**
     * Gibt die Bezeichnung zurück.
     * @return string
     */
    function GetName();
    /**
     * Gibt das Zahlensystem als Integer zurück.
     * 
     * 
     * @return int Zahlensystem-Basis
     */
    function GetSystem();
    /**
     * Gibt das höchstmögliche Zeichen zurück.
     * 
     * @return char höchstmögliches Zeichen
     */
    function GetMaxSign();
    /**
     * Gibt das Zeichen der Basis für den Wert $x zurück.
     *
     * @param int $x Wert x
     *
     * @return char Zeichen der Basis für den Wert x
     */
    function GetSign($x);
  }
  
  /**
   * Basis eines Zahlensystems.
   * 
   * @author A. Hermann
   * @copy Open Source
   * Zahlensysteme
   *
   * @version 1.0
   *
   */
  class Base implements IBase
  {
  	/**
  	 * Konstante, die die ASCII (und UTF-8) Position von 'A' speichert. 
  	 * @var int
  	 */
  	const A_POS_UTF8 = 65;
  	
  	/**
  	 * System-Name
  	 * @var string
  	 */
    private $systemName;
    /**
     * System als Integer-Zahl. Maximale Anzahl an Zeichen.
     * 
     * @var int
     */
    private $systemInt;
    /**
     * Höchstmögliches Zeichen.
     * 
     * @var char
     */
    private $maxSign;
    
    /**
     * Konstruktor
     * 
     * @param int $system Zahlensystem-Basis (Maximale Anzahl an Zeichen)
     * @param string $name (Optional) Bezeichnung des Zahlensystems
     */
    public function __construct($system, $name="")
    {
      $this->systemInt=intval($system);
      $this->systemName=$name;
      
      if ($name=="")
      {
        $this->systemName = sprintf("Basis %1\$s", intval($system));
      }
      $tmp = A_POS_UTF8 - 11 + intval($system);
      
      if ($system <= 10)
      {
        $this->maxSign = $system - 1;
      }
      else
      {
        $this->maxSign = mb_convert_encoding('&#' . $tmp . ';', 'UTF-8', 'HTML-ENTITIES');
      }
    }
    
    /**
     * (non-PHPdoc)
     * @see \ahbsd\Zahlensysteme\IBase::GetSign()
     */
    public function GetSign($x)
    {
      $tmp = 65-11+intval($x+1);
      $result = $x;
      
      if(intval($x) >= 10 || intval($x) < 0)
      {
        $result = mb_convert_encoding('&#' . $tmp . ';', 'UTF-8', 'HTML-ENTITIES');
      }
      
      return $result;
    }
    
    public function GetName()
    {
      return $this->systemName;
    }
    
    public function GetSystem()
    {
      return $this->systemInt;
    }
    
    public function GetMaxSign()
    {
      return $this->maxSign;
    }
    
    /**
     * Statische Funktion zur Umwandlung einer Zahl aus dem Dezimalsystem in eine
     * Zahl des Zahlensystems bX.
     * 
     * @param int $b10 Umzuwandelnde Zahl aus dem Dezimalsystem.
     * @param int $bX Zahlensystem in das b10 umgewandelt werden soll.
     * @param bool $rechenweg (Optional) Gibt an, ob der Rechenweg angezeigt werden soll oder nicht; ohne Angabe standardmäßig FALSE. 
     * @return string Ergebnis in Basis bX
     */
    public static function Base10toBaseX($b10, $bX, $rechenweg=false)
    {
      $targetBase = new Base(intval($bX));
      $result = array();
      $restArray = array();
      $rOut = "";
      
      $quotient = intval($b10);
      $rest = 0;
      $cnt = 0;
      
      if ($rechenweg) 
      {
        echo "\n<!-- start Rechenweg --><pre>\n";
        echo "Rechenweg:\n";
      }
      
      while (intval($quotient) != 0)
      {
        $rest = $quotient % $bX;
        if ($rechenweg) echo "$quotient : $bX = " . intval($quotient / $bX) . " Rest $rest [" . $targetBase->GetSign($rest) . "]\n";
        $restArray[] = $rest;
        $quotient = intval($quotient / $bX);
      }
      if ($rechenweg) echo "--------------------\n";
      $cnt = count($restArray);
      
      for($i=0;$i < $cnt; $i++)
      {
        $result[$cnt - ($i + 1)] = $targetBase->GetSign($restArray[$i]);
      }  
      
      for($i=0; $i < $cnt; $i++)
      {
        $rOut .= $result[$i];
      }
      
      if ($rechenweg)
      {
        printf("Das Ergebnis der Umwandlung von %1\$s der Basis 10 in die %2\$s ist '%3\$s'\n", $b10, $targetBase->GetName(), $rOut);
        echo "</pre><!-- ende Rechenweg -->\n\n";
      }
      return $rOut;
    }
    
    /**
     * 
     * @param string $bxVal Der Wert der Basis $bX
     * @param int $bX Die Quell Basis.
     * @param bool $rechenweg Gibt an, ob der Rechenweg ausgegeben werden soll, 
     * 	oder nicht.
     * @return int Der Wert $bxVal umgerechnet in Basis 10.
     */
    public static function BaseXtoBase10($bxVal, $bX, $rechenweg=false)
    {
    	$sourceBase = new Base(intval($bX));
    	$step = strlen($bxVal) - 1;
    	$result = 0;
    	$z = 0;
    	$curCarCorrect = false;
    	$intW = 0;
    	
    	if ($rechenweg)
    	{
    		echo "\n<!-- start Rechenweg --><pre>\n";
    		echo "Rechenweg:\n";
    	}
    	
    	while ($step >= 0) {
    		$charW = $bxVal[$step];
    		// echo "charW=$charW\n";
    		$tmpIntVal = intval($charW, 10);
    		//	echo "tmpIntVal='$tmpIntVal'\n";
    		
    		$curCarCorrect = ('' . $tmpIntVal . ''==$charW); 
    		//	echo "curCarCorrect=" . ($curCarCorrect==1 ? 'TRUE' : 'FALSE') . "\n";
    		
    		if ($rechenweg) printf("%3\$s) Zeichen '%1\$s' an Stelle %2\$s ", $charW, $step, $z+1); 
    		
    		if ($curCarCorrect) {
    			$intW = $tmpIntVal;
    			// echo "intW=" . $intW . "\n";
    		}
    		else {
    			// CharWert umrechnung
    			$tmp2 = ord($charW);
    			/* $tmp2 = mb_convert_encoding('' . $charW . '', 'HTML-ENTITIES', 'UTF-8');
    			echo "tmp2='$tmp2'\n";
    			$tmp2 = str_replace(';', '', $tmp2);
    			echo "tmp2='$tmp2'\n";
    			$tmp2 = str_replace('&#', '', $tmp2); */
    			// echo "tmp2='$tmp2'\n";
    			
    			$intW = intval($tmp2) - 65 + 10; // A_POS_UTF8;
    			// echo "intW='$intW'\n";
    			
    			if ($rechenweg) printf("= (int) %1\$s", $intW);
    		}
    		
    		if ($rechenweg) echo "\n";
    		
    		$tmpR = 1;
    		
    		for ($i = 0; $i < $z; $i++) {
    			$tmpR = $tmpR * $bX;
    		}
    		
    		if ($rechenweg) printf("%1\$s^%2\$s=%3\$s\n%3\$s * %4\$s = ", $bX, $z, $tmpR, $intW);
    		
    		$tmpR = $tmpR * $intW;
    		
    		if ($rechenweg) echo $tmpR . "\n\n";
    		
    		$step--;
    		$result += $tmpR;
    		$z++;
    	}
    	
    	if ($rechenweg)
    	{
    		printf("Das Ergebnis der Umwandlung von '%1\$s' der %2\$s in die Basis 10 ist %3\$s\n", $bxVal, $sourceBase->GetName(), $result);
    		echo "</pre><!-- ende Rechenweg -->\n\n";
    	}
    	
    	return $result;
    }
  }
  
  /*
  $b = 36;
  $v = 701332102;
  
  $test = new Base($b);
  
  print_r($test);
  echo "\nUmwandlung von Basis 10 in Basis " . $b . "\n";
  printf("10x%2\$s = %1\$sx%3\$s", $b, $v, $test::Base10toBaseX($v, $b, true));
  */
}
?>
