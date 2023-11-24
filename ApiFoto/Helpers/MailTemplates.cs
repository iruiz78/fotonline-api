namespace ApiFoto.Helpers
{
    public class MailTemplates
    {
        public static string ResetPassword(string code)
        {
            return $@"<!DOCTYPE html>
                    <html>
                    <head>
                        <meta charset=""utf-8"">
                        <title>Recuperación de Contraseña</title>
                        <style>
                            /* Estilos CSS para el contenedor principal */
                            .container {{
                                width: 100%;
                                max-width: 600px;
                                margin: 0 auto;
                                padding: 20px;
                                font-family: Arial, sans-serif;
                            }}

                            /* Estilos CSS para el título */
                            .title {{
                                font-size: 24px;
                                font-weight: bold;
                                color: #333;
                            }}

                            /* Estilos CSS para la descripción */
                            .description {{
                                font-size: 16px;
                                color: #666;
                            }}

                            /* Estilos CSS para el cuadro de código */
                            .code-box {{
                                background-color: #f0f0f0;
                                padding: 10px;
                                border: 1px solid #ddd;
                                margin: 20px 0;
                            }}

                            /* Estilos CSS para el código */
                            .code {{
                                font-size: 28px;
                                font-weight: bold;
                                color: #333;
                                text-align:center;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class=""container"">
                            <div class=""title"">Recuperación de Contraseña</div>
                            <div class=""description"">Hemos recibido una solicitud para restablecer tu contraseña. A continuación, encontrarás el código de recuperación:</div>
                            <div class=""code-box"">
                                <div class=""code"">{code}</div>
                            </div>
                            <div class=""description"">Si no has solicitado esta recuperación de contraseña, puedes ignorar este mensaje.</div>
                        </div>
                    </body>
                    </html>";
        }
    }
}
