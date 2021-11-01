namespace SystemyWP.API.Utilities
{
    public static class EmailTemplates
    {
        public static string InvitationEmailBody(string url)
        {
            return
                $"<table><tr><td style=height:20px;>&nbsp;</td></tr><tr><td style=text-align:center;><h1 style= font-size: 25px;>Systemy Wspomagania Pracy</h1></td></tr><tr><td style= height:20px;></hr></td></tr><tr><td><table style= width:95%; max-width:670px; ><tr><td style=height:40px;></hr></td></tr><tr><td style= text-align:center; padding:0 35px;><h1 style= color:#1e1e2d; font-weight:500; margin:0; font-size:32px; text-align:center;>Rejestracja użytkownika na portalu Systemy Wspomagania Pracy</h1><span style= display:inline-block; vertical-align:middle; margin:29px 0 26px; border-bottom:1px solid #cecece; width:100px;></span><p style=color:#455056; font-size:15px;line-height:24px; margin:0;>Klikając w poniższy link potwierdzą Państwo chęć rejestracji na portalu. Jeśli to nie Państwo skorzystali z formularza rejestracji na naszym portalu prosimy o zignorowanie tej wiadomości.</p><a style= background-color: black; border: none; color: white; padding: 15px 32px; text-align: center; text-decoration: none; display: inline-block; font-size: 16px; margin: 4px 2px; cursor: pointer; href=\"{url}\">POTWIERDZAM REJESTRACJĘ</a><p style=color:#455056; font-size:15px;line-height:24px; margin-top:10px;>Wiadomość została wygenerowana automatycznie. Prosimy na nią nie odpowiadać.</p><p style=color:#455056; font-size:15px;line-height:24px; margin-top: 10px;>Z poważaniem,</p><p style=color:#455056; font-size:15px;line-height:24px; margin:0;>Zespół SWP</p></table></td><tr><td style=height:20px;>&nbsp;</td></tr><tr><td></td></tr><tr><td style=height:80px;>&nbsp;</td></tr></table>";
        }

        public static string PasswordResetEmailBody(string url)
        {
            return
                $"<div><hr/><h4 style = padding-bottom: 5pex;> Szanowny Użytkowniku,</h4 ><p> Otrzymaliśmy prośbę resetu hasła do Twojego konta na portalu Systemy Wspomagania Pracy.</p><p> W celu zresetowania hasła prosimy kliknąć w poniższy link.</p><p style = padding: 20px 0px 20px 0px;><a style=background:#0067b8; text-decoration:none !important; font-weight:500; margin-top:35px; color:#fff;text-transform:uppercase; font-size:14px;padding:10px 24px;display:inline-block; border-radius:50px; href =\"{url}\">Zmiana hasła.</a></p><p> Jeśli nie korzystałeś z funkcji 'Zapomniałeś hasła?' na naszym portalu prosimy o zignorowanie tej wiadomości.</p><p> Wiadomość została wygenerowana automatycznie. Prosimy na nią nie odpowiadać.</p><p> Z poważaniem,</p><p> systemywp.pl </p><hr/></div>";
        }
    }
}