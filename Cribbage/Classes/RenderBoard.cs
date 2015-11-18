using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Reflection;

namespace Cribbage.Classes
{
    public class RenderBoard
    {
        static public System.Web.UI.WebControls.Image UpdateBoard(BoardStatus boardStatus)
        {
            Bitmap bmp = new Bitmap(HttpContext.Current.Server.MapPath(@"images/cribbage_board.jpg"));
            Graphics g = Graphics.FromImage(bmp);

            int[] location = new int[2];
            int[] location2 = new int[2];
            int[] location3 = new int[2];
            int[] location4 = new int[2];

            PropertyInfo[] properties = typeof(BoardStatus).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                dynamic value = property.GetValue(boardStatus);

                switch (property.Name)
                {
                    case "P1FirstPeg":
                        {
                            location = GetPegLocation(1, value);
                            break;
                        }
                    case "P1SecondPeg":
                        {
                            location2 = GetPegLocation(1, value);
                            break;
                        }
                    case "P2FirstPeg":
                        {
                            location3 = GetPegLocation(2, value);
                            break;
                        }
                    case "P2SecondPeg":
                        {
                            location4 = GetPegLocation(2, value);
                            break;
                        }
                    default:
                        break;
                }
            }

            g.FillEllipse(boardStatus.P1Color, location[0], location[1], 10, 10);
            g.FillEllipse(boardStatus.P1Color, location2[0], location2[1], 10, 10);
            g.FillEllipse(boardStatus.P2Color, location3[0], location3[1], 10, 10);
            g.FillEllipse(boardStatus.P2Color, location4[0], location4[1], 10, 10);

            System.Web.UI.WebControls.Image board = new System.Web.UI.WebControls.Image();
            using (MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();
                Convert.ToBase64String(byteImage);
                board.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            }

            return board;
        }

        static private int[] GetPegLocation(int Player, int PegLocation)
        {
            int[] coords = new int[2];
            
            switch (Player)
            {
                case 1:
                    {
                        switch (PegLocation)
                        {
                            case 0:
                                {
                                    coords[0] = 36;
                                    coords[1] = 34;
                                    break;
                                }
                            case 1:
                                {
                                    coords[0] = 48;
                                    coords[1] = 34;
                                    break;
                                }
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                                {
                                    coords[0] = 68 + (12 * (PegLocation - 2));
                                    coords[1] = 34;


                                    break;
                                }
                            case 7:
                            case 8:
                            case 9:
                            case 10:
                            case 11:
                                {
                                    coords[0] = 137 + (12 * (PegLocation - 7));
                                    coords[1] = 34;
                                    break;
                                }
                            case 12:
                            case 13:
                            case 14:
                            case 15:
                            case 16:
                                {
                                    coords[0] = 206 + (12 * (PegLocation - 12));
                                    coords[1] = 34;
                                    break;
                                }
                            case 17:
                            case 18:
                            case 19:
                            case 20:
                            case 21:
                                {
                                    coords[0] = 275 + (12 * (PegLocation - 17));
                                    coords[1] = 34;
                                    break;
                                }
                            case 22:
                            case 23:
                            case 24:
                            case 25:
                            case 26:
                                {
                                    coords[0] = 344 + (12 * (PegLocation - 22));
                                    coords[1] = 34;
                                    break;
                                }
                            case 27:
                            case 28:
                            case 29:
                            case 30:
                            case 31:
                                {
                                    coords[0] = 413 + (12 * (PegLocation - 27));
                                    coords[1] = 34;
                                    break;
                                }
                            case 32:
                            case 33:
                            case 34:
                            case 35:
                            case 36:
                                {
                                    coords[0] = 482 + (12 * (PegLocation - 32));
                                    coords[1] = 34;
                                    break;
                                }
                            
                            
                            
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        break;
                    }
                case 2:
                    {
                        switch (PegLocation)
                        {
                            case 0:
                                {
                                    coords[0] = 36;
                                    coords[1] = 300;
                                    break;
                                }
                            case 1:
                                {
                                    coords[0] = 48;
                                    coords[1] = 300;
                                    break;
                                }
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                                {
                                    coords[0] = 68 + (12 * (PegLocation - 2));
                                    coords[1] = 300;


                                    break;
                                }
                            case 7:
                            case 8:
                            case 9:
                            case 10:
                            case 11:
                                {
                                    coords[0] = 137 + (12 * (PegLocation - 7));
                                    coords[1] = 300;
                                    break;
                                }
                            case 12:
                            case 13:
                            case 14:
                            case 15:
                            case 16:
                                {
                                    coords[0] = 206 + (12 * (PegLocation - 12));
                                    coords[1] = 300;
                                    break;
                                }
                            case 17:
                            case 18:
                            case 19:
                            case 20:
                            case 21:
                                {
                                    coords[0] = 275 + (12 * (PegLocation - 17));
                                    coords[1] = 300;
                                    break;
                                }
                            case 22:
                            case 23:
                            case 24:
                            case 25:
                            case 26:
                                {
                                    coords[0] = 344 + (12 * (PegLocation - 22));
                                    coords[1] = 300;
                                    break;
                                }
                            case 27:
                            case 28:
                            case 29:
                            case 30:
                            case 31:
                                {
                                    coords[0] = 413 + (12 * (PegLocation - 27));
                                    coords[1] = 300;
                                    break;
                                }
                            case 32:
                            case 33:
                            case 34:
                            case 35:
                            case 36:
                                {
                                    coords[0] = 482 + (12 * (PegLocation - 32));
                                    coords[1] = 300;
                                    break;
                                }

                            
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return coords;
        }
    }
}