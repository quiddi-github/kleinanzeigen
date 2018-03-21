using System;
using System.Net;// für WebClient
using System.Text.RegularExpressions; //für Regex und Match
using System.IO;
using System.Text;
 
public class HelloWorld
{


    static public void Main ()
    {
        Console.WriteLine ("Programm startet");
        Webseite testwebseite = new Webseite();

        string url = "";
        int artikel = 0;
        int seiten = 0;
        string suche = "";
        Console.WriteLine("Bitte Url eingeben");
        url = Console.ReadLine();
        Console.WriteLine("Bitte Artikelanzahl pro Seite eingeben");
        artikel = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Bitte zu durchsuchende Seiten eingeben");
        seiten = Convert.ToInt32(Console.ReadLine());              
        Console.WriteLine("Bitte Suchbegriff eingeben");
        suche = Console.ReadLine();



		testwebseite.starteProgramm(url,artikel,seiten,suche);
    }





    class Webseite
    	{	
    			WebClient wClient2 = new WebClient();
    			private int zeilennummer_1 = 0;
    			private int artikel_pro_seite = 0;
    			private string webseiteUrl;
    			private string[] urlArtikel = new string[27];
    			private string[] preisArtikel = new string[27];
    			private string[] ueberschriftArtikel = new string[27];
    			private string[] beschreibungArtikel = new string[27];
    			private int[] wahrheit = new int[27];
    			private string[] urlImage = new string[27];
    			private string hauptseite;

    			public void variablenLeeren()
    				{
    					int i = 0;
    					while(i<artikel_pro_seite)
    						{
    							urlArtikel[i] = "";
    							preisArtikel[i] = "";
    							ueberschriftArtikel[i] = "";
    							beschreibungArtikel[i] = "";
    							wahrheit[i] = 0;
    							urlImage[i] = "";
    							hauptseite = "";
    							i++;
    						}
    				}

    			public void set_ArtProSeite(int a)
    				{
    					artikel_pro_seite = a;
    				}
    			public void set_WebseiteUrl(string a, int artProSeite)
    				{
    					webseiteUrl = a;
    					artikel_pro_seite = artProSeite;
    					if(artikel_pro_seite > 25)
    						{
    							wahrheit[0] = 1;
    							wahrheit[1] = 1;
    						}
    				}
    			public void set_hauptseite(string a)
    				{
    					hauptseite = a;
    				}    				

    		    public string webseite_lesen(string url)
    				{

        				string strSource = wClient2.DownloadString(url);
        				return strSource;
    				}
    			public void testzeichen()
   					{
      					String str = "ainnnmal";
      					String toFind = "n";
      					int index = str.IndexOf("n");
      					Console.WriteLine("Found '{0}' in '{1}' at position {2}",
                        toFind, str, index);
   					}
   				public void suchen(string eingabe)
   					{
   						       // The input string.
        				string s = eingabe;

        					// Loop through all instances of the letter a.
        				int i = 0;
        				while ((i = s.IndexOf("data-href=", i)) != -1)
        					{
            				// Print out the substring.
            					Console.WriteLine(s.Substring(i));

            					// Increment the index.
            					i++;
        					}
   					}


   				public string getBetween(string strSource, string strStart, string strEnd, int zeichennummer)
					{
    					int Start, End;
    							if (strSource.Contains(strStart) && strSource.Contains(strEnd))
    								{

        								Start = strSource.IndexOf(strStart, zeichennummer) + strStart.Length;
        								End = strSource.IndexOf(strEnd, Start);
        								zeilennummer_1 = End;
        								//Console.WriteLine(strSource.Substring(Start, End - Start));
        								return strSource.Substring(Start, End - Start);
    								}
    							else
    								{
        								return "";
    								}
							

					}

				public void liesBeschreibungWahr(string wort)
					{
						int i = 0;
						while(i < artikel_pro_seite)
							{
								if(beschreibungArtikel[i].IndexOf(wort, StringComparison.InvariantCultureIgnoreCase)==(-1))
									{
										wahrheit[i] = 1;
									}
								i++;
							}
					}
				public void liesBeschreibungFalsch(string wort)
					{
						int i = 0;
						while(i < artikel_pro_seite)
							{
								if(beschreibungArtikel[i].IndexOf(wort, StringComparison.InvariantCultureIgnoreCase)>0)
									{
										wahrheit[i] = 1;
									}
								i++;
							}
					}

   				public void getAdressen(string strStart, string strEnd)
					{
    					int i = 0;
    					zeilennummer_1 = 0;
    					while(i < (artikel_pro_seite))
    						{
    							//getBetween(strSource, strStart, strEnd, zeilennummer_1);
    							urlArtikel[i] = "https://www.ebay-kleinanzeigen.de" + getBetween(hauptseite,strStart, strEnd, zeilennummer_1);
    							i++;

								//getBetween()

							}
					}
				public void getBilder()
					{
						int i = 0;
    					zeilennummer_1 = 0;
    					while(i < artikel_pro_seite)
    						{
    							
								//if(zeilennummer_1 > 200)
								//	{
    							//		zeilennummer_1 = zeilennummer_1 -120;
    							//	}
    							string linkImage = getBetween(hauptseite,"data-href=\"", "\"", zeilennummer_1);
    							int zeilennummer_2 = zeilennummer_1;
    							linkImage = getBetween(hauptseite,"data-imgsrc=\"", "\"", zeilennummer_1);
 								int differenz = Math.Abs(zeilennummer_1 - zeilennummer_2);
 								//Console.WriteLine(differenz);
    							   					//Console.WriteLine(i);

    							//linkImage = getBetween(hauptseite,"data-imgsrc=\"", "\"", zeilennummer_1);
    							if(differenz < 500 )
    								{
    									urlImage[i] = linkImage;
    								}
    							else
    								{
    									urlImage[i] = "";
    									if(artikel_pro_seite -1 > i)
    										{
    											urlImage[i+1] = linkImage;
    											i++;
    										}
    								}
    							i++;
    							//Console.WriteLine(i);
							}

					}

    				
    			public void printUrls()
    				{
    					int i = 0;
    					while(i < artikel_pro_seite)
    						{
    							//Console.WriteLine(urlArtikel[i]);
    							i++;
    						}
    				}
    			public void getPreis()
    				{
    					int i = 0;
    					while(i < artikel_pro_seite)
    						{
    							zeilennummer_1 = 0;
    							string inhaltWebseite = webseite_lesen(urlArtikel[i]);
    							ueberschriftArtikel[i] = getBetween(inhaltWebseite, "<title>", "</title>", zeilennummer_1);
    							preisArtikel[i] = getBetween(inhaltWebseite, "id=\"viewad-price\">Preis: ", "</h2>", zeilennummer_1);
    							beschreibungArtikel[i] = getBetween(inhaltWebseite, "class=\"text-force-linebreak\" itemprop=\"description\">", "</p>", zeilennummer_1);
    							

    							i++;
    						}
    				}
    			public void printPreis()
    				{
    					int i = 0;
    					while(i < artikel_pro_seite)
    						{
    							Console.WriteLine(preisArtikel[i]);
    							i++;
    						}

    				}
    			public void printBeschreibung()
    				{
    					int i = 0;
    					while(i < artikel_pro_seite)
    						{
    							Console.WriteLine(beschreibungArtikel[i]);
    							i++;
    						}
    				}
    			public void printWebpageJaNein()
    				{
    					int i = 0;
    					while(i < artikel_pro_seite)
    						{
    							Console.WriteLine(wahrheit[i]);
    							i++;
    						}
    				}
    			public void erzeugeHTML()
    				{
    					string path = @"ausgabe.html";
    					if(File.Exists(@"ausgabe.html"))
							{
    							File.Delete(@"ausgabe.html");
							}

						if (!File.Exists(path))
        					{
            					string createText = "eBay Kleinanzeigen Suchmaschine <br />" + Environment.NewLine;
            					File.WriteAllText(path, createText);
        					}
    				}




    			public void ausgabeHTMLeinArtikel(int index)
    				{
    					string path = @"ausgabe.html";
        				string appendText = "";
        				//File.AppendAllText(path, appendText);
        				appendText = "<b>" + "<font color=\"#FF0000\">" + ueberschriftArtikel[index] + "<br />" + Environment.NewLine + preisArtikel[index] + "</font>" + "</b>" + "<br />" + "<a href=\"" + urlArtikel[index] + "\">zum Artikel</a>" + "<br />" + "<img src=\"" + urlImage[index] + "\" alt=\"Kein Bild\" width=\"100\" height=\"100\">" + "<br />" + beschreibungArtikel[index] + "<br />" + "<br />" + "<br />";
        				File.AppendAllText(path, appendText);

        				//Console.WriteLine(index);

    				}
    			public void ausgabeHTML(int seiten)
    				{
    					//int seitencounter = 0;
    					//while(seitencounter < seiten)
    					//	{
    							int i = 0;
    							while(i < artikel_pro_seite)
    								{
    									if(wahrheit[i] == 0)
    										{
    											ausgabeHTMLeinArtikel(i);

    										}
    									i++;
    								}
    					//		seitencounter++;
    					//	}
    				}
    			public void starteProgramm(string url, int anzahlArtikel, int seiten, string suche)
    				{
    					variablenLeeren();
    					set_WebseiteUrl(url,anzahlArtikel);
    					Console.WriteLine("Verarbeite Seite 1...");
    					set_ArtProSeite(anzahlArtikel);
    					hauptseite = webseite_lesen(url);
    					
    					getAdressen("data-href=\"", "\"");
    					getPreis();
    					getBilder();
    					liesBeschreibungWahr(suche);
    					Console.WriteLine("Suche auf Seite 1 abgeschlossen.");
    					erzeugeHTML();
    					ausgabeHTML(seiten);
    					if(seiten > 1)
    						{	
    							int i = 1;
    							while(i < seiten)
    								{	
    									Console.WriteLine(url);
    									artikel_pro_seite = anzahlArtikel;
    									variablenLeeren();
    									
    									////string url2 = url.Subtring(34);
    									url = url.Substring(34);
    									Console.WriteLine(url);
    									url = "https://www.ebay-kleinanzeigen.de/" + "seite:" + (i+1) + "/" + url;
    									Console.WriteLine(url);
    									set_WebseiteUrl(url,anzahlArtikel);
    									Console.WriteLine("Verarbeite Seite " + (i+1) + "...");
    									set_ArtProSeite(anzahlArtikel);
    									hauptseite = webseite_lesen(url);
    									getAdressen("data-href=\"", "\"");
    									getPreis();
    									getBilder();
    									liesBeschreibungWahr(suche);
    									Console.WriteLine("Suche auf Seite " + (i+1) + " abgeschlossen");
    									//erzeugeHTML();
    									   					string path = @"ausgabe.html";

						if (File.Exists(path))
        					{
            					string createText = "Seite 2 <br />" + Environment.NewLine;
            					//File.WriteAllText(path, createText);
            					File.AppendAllText(path, createText);
        					}
    									ausgabeHTML(seiten);
    									i++;
    								}
    						}

    					
    				}


    	}
}