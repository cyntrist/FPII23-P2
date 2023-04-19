//Paula Sierra Luque
//Cynthia Tristán Álvarez
using System.Text.RegularExpressions;
using Listas;
using System.Windows;

internal class AutocompletadoComandos
//Autocompletado de comandos: en los prompts textuales es útil tener una opción 
//de autocompletado con el tabulador: 
//se teclean uno o varios caracteres y con el tabulador se buscan comandos
//con ese prefijo; si el comando es único (no hay ambigüedad) se completa, si hay varios se dan
//las opciones.
{
    private ListaEnlazada listaComandos;

    public AutocompletadoComandos(ListaEnlazada comandos) //constructora
    {
        listaComandos = comandos;
    }

    public ListaEnlazada BuscarComandos(string prefijo)
    //al pulsar tab, se lee el string y miramos si coincide con algún comando
    {
        string comando;
        if
        return comando;
    }
    public bool PulsaTab()
    {
        bool tab = false;

        return tab;
    }
}