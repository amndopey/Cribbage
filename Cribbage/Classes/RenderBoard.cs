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
                            case 37:
                                {
                                    coords[0] = 548;
                                    coords[1] = 37;
                                    break;
                                }
                            case 38:
                                {
                                    coords[0] = 556;
                                    coords[1] = 46;
                                    break;
                                }
                            case 39:
                                {
                                    coords[0] = 561;
                                    coords[1] = 57;
                                    break;
                                }
                            case 40:
                                {
                                    coords[0] = 558;
                                    coords[1] = 68;
                                    break;
                                }
                            case 41:
                                {
                                    coords[0] = 549;
                                    coords[1] = 75;
                                    break;
                                }
                            case 42:
                            case 43:
                            case 44:
                            case 45:
                            case 46:
                                {
                                    coords[0] = 530 - (12 * (PegLocation - 42));
                                    coords[1] = 79;
                                    break;
                                }
                            case 47:
                            case 48:
                            case 49:
                            case 50:
                            case 51:
                                {
                                    coords[0] = 523 - (12 * (PegLocation - 42));
                                    coords[1] = 77;
                                    break;
                                }
                            case 52:
                            case 53:
                            case 54:
                            case 55:
                            case 56:
                                {
                                    coords[0] = 514 - (12 * (PegLocation - 42));
                                    coords[1] = 77;
                                    break;
                                }
                            case 57:
                            case 58:
                            case 59:
                            case 60:
                            case 61:
                                {
                                    coords[0] = 504 - (12 * (PegLocation - 42));
                                    coords[1] = 77;
                                    break;
                                }
                            case 62:
                            case 63:
                            case 64:
                            case 65:
                            case 66:
                                {
                                    coords[0] = 494 - (12 * (PegLocation - 42));
                                    coords[1] = 77;
                                    break;
                                }
                            case 67:
                            case 68:
                            case 69:
                            case 70:
                            case 71:
                                {
                                    coords[0] = 484 - (12 * (PegLocation - 42));
                                    coords[1] = 77;
                                    break;
                                }
                            case 72:
                            case 73:
                            case 74:
                            case 75:
                            case 76:
                                {
                                    coords[0] = 474 - (12 * (PegLocation - 42));
                                    coords[1] = 77;
                                    break;
                                }
                            case 77:
                                {
                                    coords[0] = 47;
                                    coords[1] = 80;
                                    break;
                                }
                            case 78:
                                {
                                    coords[0] = 37;
                                    coords[1] = 88;
                                    break;
                                }
                            case 79:
                                {
                                    coords[0] = 34;
                                    coords[1] = 100;
                                    break;
                                }
                            case 80:
                                {
                                    coords[0] = 37;
                                    coords[1] = 110;
                                    break;
                                }
                            case 81:
                                {
                                    coords[0] = 47;
                                    coords[1] = 118;
                                    break;
                                }
                            case 82:
                            case 83:
                            case 84:
                            case 85:
                            case 86:
                                {
                                    coords[0] = 68 + (12 * (PegLocation - 82));
                                    coords[1] = 122;
                                    break;
                                }
                            case 87:
                            case 88:
                            case 89:
                            case 90:
                            case 91:
                                {
                                    coords[0] = 137 + (12 * (PegLocation - 87));
                                    coords[1] = 122;
                                    break;
                                }
                            case 92:
                            case 93:
                            case 94:
                            case 95:
                            case 96:
                                {
                                    coords[0] = 206 + (12 * (PegLocation - 92));
                                    coords[1] = 122;
                                    break;
                                }
                            case 97:
                            case 98:
                            case 99:
                            case 100:
                            case 101:
                                {
                                    coords[0] = 275 + (12 * (PegLocation - 97));
                                    coords[1] = 122;
                                    break;
                                }
                            case 102:
                            case 103:
                            case 104:
                            case 105:
                            case 106:
                                {
                                    coords[0] = 346 + (12 * (PegLocation - 102));
                                    coords[1] = 122;
                                    break;
                                }
                            case 107:
                            case 108:
                            case 109:
                            case 110:
                            case 111:
                                {
                                    coords[0] = 415 + (12 * (PegLocation - 107));
                                    coords[1] = 122;
                                    break;
                                }
                            case 112:
                            case 113:
                            case 114:
                            case 115:
                            case 116:
                                {
                                    coords[0] = 483 + (12 * (PegLocation - 112));
                                    coords[1] = 122;
                                    break;
                                }
                            case 117:
                                {
                                    coords[0] = 550;
                                    coords[1] = 124;
                                    break;
                                }
                            case 118:
                                {
                                    coords[0] = 559;
                                    coords[1] = 133;
                                    break;
                                }
                            case 119:
                                {
                                    coords[0] = 562;
                                    coords[1] = 143;
                                    break;
                                }
                            case 120:
                                {
                                    coords[0] = 559;
                                    coords[1] = 154;
                                    break;
                                }
                            case 121:
                                {
                                    coords[0] = 550;
                                    coords[1] = 161;
                                    break;
                                }
                            case 122:
                                {
                                    coords[0] = 530;
                                    coords[1] = 166;
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
                            case 37:
                                {
                                    coords[0] = 550;
                                    coords[1] = 297;
                                    break;
                                }
                            case 38:
                                {
                                    coords[0] = 560;
                                    coords[1] = 287;
                                    break;
                                }
                            case 39:
                                {
                                    coords[0] = 562;
                                    coords[1] = 277;
                                    break;
                                }
                            case 40:
                                {
                                    coords[0] = 559;
                                    coords[1] = 266;
                                    break;
                                }
                            case 41:
                                {
                                    coords[0] = 551;
                                    coords[1] = 258;
                                    break;
                                }
                            case 42:
                            case 43:
                            case 44:
                            case 45:
                            case 46:
                                {
                                    coords[0] = 530 - (12 * (PegLocation - 42));
                                    coords[1] = 255;
                                    break;
                                }
                            case 47:
                            case 48:
                            case 49:
                            case 50:
                            case 51:
                                {
                                    coords[0] = 523 - (12 * (PegLocation - 42));
                                    coords[1] = 255;
                                    break;
                                }
                            case 52:
                            case 53:
                            case 54:
                            case 55:
                            case 56:
                                {
                                    coords[0] = 514 - (12 * (PegLocation - 42));
                                    coords[1] = 256;
                                    break;
                                }
                            case 57:
                            case 58:
                            case 59:
                            case 60:
                            case 61:
                                {
                                    coords[0] = 504 - (12 * (PegLocation - 42));
                                    coords[1] = 256;
                                    break;
                                }
                            case 62:
                            case 63:
                            case 64:
                            case 65:
                            case 66:
                                {
                                    coords[0] = 494 - (12 * (PegLocation - 42));
                                    coords[1] = 256;
                                    break;
                                }
                            case 67:
                            case 68:
                            case 69:
                            case 70:
                            case 71:
                                {
                                    coords[0] = 484 - (12 * (PegLocation - 42));
                                    coords[1] = 256;
                                    break;
                                }
                            case 72:
                            case 73:
                            case 74:
                            case 75:
                            case 76:
                                {
                                    coords[0] = 474 - (12 * (PegLocation - 42));
                                    coords[1] = 256;
                                    break;
                                }
                            case 77:
                                {
                                    coords[0] = 46;
                                    coords[1] = 255;
                                    break;
                                }
                            case 78:
                                {
                                    coords[0] = 36;
                                    coords[1] = 246;
                                    break;
                                }
                            case 79:
                                {
                                    coords[0] = 33;
                                    coords[1] = 235;
                                    break;
                                }
                            case 80:
                                {
                                    coords[0] = 36;
                                    coords[1] = 224;
                                    break;
                                }
                            case 81:
                                {
                                    coords[0] = 45;
                                    coords[1] = 215;
                                    break;
                                }
                            case 82:
                            case 83:
                            case 84:
                            case 85:
                            case 86:
                                {
                                    coords[0] = 67 + (12 * (PegLocation - 82));
                                    coords[1] = 211;
                                    break;
                                }
                            case 87:
                            case 88:
                            case 89:
                            case 90:
                            case 91:
                                {
                                    coords[0] = 135 + (12 * (PegLocation - 87));
                                    coords[1] = 211;
                                    break;
                                }
                            case 92:
                            case 93:
                            case 94:
                            case 95:
                            case 96:
                                {
                                    coords[0] = 206 + (12 * (PegLocation - 92));
                                    coords[1] = 211;
                                    break;
                                }
                            case 97:
                            case 98:
                            case 99:
                            case 100:
                            case 101:
                                {
                                    coords[0] = 275 + (12 * (PegLocation - 97));
                                    coords[1] = 211;
                                    break;
                                }
                            case 102:
                            case 103:
                            case 104:
                            case 105:
                            case 106:
                                {
                                    coords[0] = 346 + (12 * (PegLocation - 102));
                                    coords[1] = 211;
                                    break;
                                }
                            case 107:
                            case 108:
                            case 109:
                            case 110:
                            case 111:
                                {
                                    coords[0] = 415 + (12 * (PegLocation - 107));
                                    coords[1] = 211;
                                    break;
                                }
                            case 112:
                            case 113:
                            case 114:
                            case 115:
                            case 116:
                                {
                                    coords[0] = 483 + (12 * (PegLocation - 112));
                                    coords[1] = 211;
                                    break;
                                }
                            case 117:
                                {
                                    coords[0] = 551;
                                    coords[1] = 209;
                                    break;
                                }
                            case 118:
                                {
                                    coords[0] = 560;
                                    coords[1] = 202;
                                    break;
                                }
                            case 119:
                                {
                                    coords[0] = 563;
                                    coords[1] = 191;
                                    break;
                                }
                            case 120:
                                {
                                    coords[0] = 559;
                                    coords[1] = 180;
                                    break;
                                }
                            case 121:
                                {
                                    coords[0] = 551;
                                    coords[1] = 171;
                                    break;
                                }
                            case 122:
                                {
                                    coords[0] = 530;
                                    coords[1] = 166;
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