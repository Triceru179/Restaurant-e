<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>

    <link rel="shortcut icon" type="image/x-icon" href="/images/logo-claro.png" />

    <link rel="stylesheet" href="Content/bootstrap.min.css" />
    <link rel="stylesheet" href="Content/jquery.mCustomScrollbar.min.css"/>
    <link rel="stylesheet" href="Content/Pages.css" />

    <script src="Scripts/jquery-3.5.1.min.js"></script>
    <script src="Scripts/popper.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

    <script src="Scripts/jquery.mCustomScrollbar.concat.min.js"></script>
    <script src="Scripts/Pages.js"></script>

    <title>Login ~ Restaurant-e</title>
</head>
<body>
    <form id="form1" runat="server" style="height: 100vh;">
        <div class="container h-100">
            <div class="row align-items-center h-100">
                <nav class="navbar navbar-light bg-primary fixed-top navbar-expand-lg">
                    <%-- ~~~~~~~~~~~~~~~~~~~~~~~~ Logo e título na navbar ~~~~~~~~~~~~~~~~~~~~~~~~ --%>
                    <img src="/images/logo-claro.png" width="40" alt="" class="img-fluid"/>
                    <span class="navbar-brand px-3 text-white"> Restaurant-e </span>
                    <%-- ~~~~~~~~~~~~~~~~~~~~~~~~ Botão abrir navbar ~~~~~~~~~~~~~~~~~~~~~~~~ --%>
                    <button class="navbar-toggler border-light" type="button" data-toggle="collapse" data-target="#navbarTogglerDemo02" aria-controls="navbarTogglerDemo02" aria-expanded="false" aria-label="Toggle navigation">
                        <div class="text-white">
                            ☰
                        </div>
                    </button>
                    <%-- ~~~~~~~~~~~~~~~~~~~~~~~~ Navbar (celular) ~~~~~~~~~~~~~~~~~~~~~~~~ --%>
                    <div class="collapse navbar-collapse" id="navbarTogglerDemo02">
                        <ul class="navbar-nav nav">
                            <li class="btn btn-primary focus mt-2 mt-md-0 py-0">
                                <span class="nav-link text-white active"> Home </span>
                            </li>
                            <li class="btn btn-primary py-0 mx-md-2 my-2 my-md-0">
                                <a class="nav-link text-white" href="/Aboutus.aspx"> Sobre nós </a>
                            </li>
                            <li class="btn btn-primary py-0">
                                <a class="nav-link text-white" href="/Contact.aspx"> Contato </a>
                            </li>
                        </ul>
                    </div>
                </nav>
                <!-- ~~~~~~~~~~~~~~~~~~~~~~~~ Home - texto ~~~~~~~~~~~~~~~~~~~~~~~~ -->
                <div class="col-12 col-sm-10 col-md-8 col-lg-6 mx-auto mt-5">
                    <div class="">
                        <div class="">
                            <div class="text-logo-titulo text-center mb-2">
                                Restaurant-e
                            </div>
                            <div class="text-center">
                                O Restaurant-e auxilia na fluidez da execução
                                de seu estabelecimento, proporcionando controle
                                e segurança
                            </div>
                        </div>
                    </div>
                </div>

                <!-- ~~~~~~~~~~~~~~~~~~~~~~~~ Home - login ~~~~~~~~~~~~~~~~~~~~~~~~ -->
                <div class="col-12 col-sm-10 col-md-8 col-lg-5 offset-0 offset-lg-1 mx-auto mb-3">
                    <div class="card bg-primary card-signin">
                        <div class="card-body text-white">
                            <div class="h4 card-title text-center mt-2">Login</div>
                            <div class="form-label-group">
                                <label for="inputEmail" class="text-white">Email</label>
                                <asp:TextBox runat="server" ID="txtEmail" required="true" MaxLength="254" TextMode="Email" autocomplete="off" Text="israel@email.com" placeholder="meu@email.com" CssClass="form-control"></asp:TextBox>
                            </div>
                            <br />
                            <div class="form-label-group">
                                <label for="inputPassword" class="text-white">Password</label>
                                <asp:TextBox runat="server" ID="txtSenha" required="true" MaxLength="254" TextMode="Password" autocomplete="off" autofocus="true" Text="123456"  CssClass="form-control"></asp:TextBox>
                            </div>
                            <br />
                            <br />
                            <asp:Label runat="server" id="res"></asp:Label>
                            <asp:Button runat="server" ID="btnEntrar" class="btn btn-lg btn-primary btn-block" type="submit" Text="Entrar" OnClick="btnEntrar_Click"></asp:Button>
                            <div class="my-2"></div>
                            <!--div class="text-center">
                                <label class="text-center text-white"> Esqueceu sua senha ?
                                    <a href="" class="text-primary">
                                        Clique aqui
                                    </a>
                                </label>
                            </div-->
                        </div>
                    </div>
                </div>

<div role="alert" aria-live="assertive" aria-atomic="true" class="toast bg-primary" data-autohide="false" style="position: absolute; bottom: 5vh; right: 2vw;">
<div class="toast-header bg-primary">
<span class="btn-danger" style="cursor: pointer;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
<strong class="ml-2 mr-auto">Erro</strong>
<button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Close">
<span aria-hidden="true">&times;</span>
</button>
</div>
<div class="toast-body">
Usuario ou senha inválidos
</div>
</div>

                <div class="footer d-none d-md-block">
                    <span>
                        © 2020 - Restaurant-e
                    </span>
                </div>
            </div>
        </div>
    </form>
</body>
</html>