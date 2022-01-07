<?php

namespace ahbsd\Zahlensysteme;

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
     * @return string die Bezeichnung
     */
    function GetName();
    /**
     * Gibt das Zahlensystem als Integer zurück.
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
     * @return char Zeichen der Basis für den Wert x
     */
    function GetSign($x);
}