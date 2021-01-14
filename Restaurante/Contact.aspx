<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

    <link rel="shortcut icon" type="image/x-icon" href="/images/logo-claro.png" />

    <link href="//maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet">

    <link rel="stylesheet" href="Content/bootstrap.min.css" />
    <link rel="stylesheet" href="Content/jquery.mCustomScrollbar.min.css"/>
    <link rel="stylesheet" href="Content/Pages.css" />

    <script src="Scripts/jquery-3.5.1.min.js"></script>
    <script src="Scripts/popper.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

    <script src="Scripts/jquery.mCustomScrollbar.concat.min.js"></script>
    <script src="Scripts/Pages.js"></script>
    <title>Contato ~ Restaurant-e</title>
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
                            <li class="btn btn-primary mt-2 mt-md-0 py-0">
                                <a class="nav-link text-white" href="/"> Home </a>
                            </li>
                            <li class="btn btn-primary py-0 mx-md-2 my-2 my-md-0">
                                <a class="nav-link text-white" href="/Aboutus.aspx"> Sobre nós </a>
                            </li>
                            <li class="btn btn-primary focus py-0">
                                <span class="nav-link text-white active"> Contato </span>
                            </li>
                        </ul>
                    </div>
                </nav>

                <%-- ~~~~~~~~~~~~~~~~~~~~~~~~ Home - Contact ~~~~~~~~~~~~~~~~~~~~~~~~ --%>
                <div class="col-12 col-md-8 mx-auto">
                    <div class="card bg-primary px-1 mx-0">
                        <div class="card-header text-center">
                            <img src="/images/logo-claro.png" alt="/images/profile.png" class="rounded-circle" width="200px"/>
                        </div>
                        <div class="card-body text-center">
                            <label> Restaurant-e </label>
                        </div>
                        <div class="card-footer">
                            <div class="row">
                                <div class="col-12">
                                    <i class="fa fa-envelope-o" aria-hidden="true"></i>&nbsp;Email:
                                    <span class="text-primary">restaurant-e@gmail.com</span>
                                    <br />
                                    <i class="fa fa-facebook-square" aria-hidden="true"></i>&nbsp;Facebook:
                                    <span class="text-primary">https://pt-br.facebook.com/RestaurantE</span>
                                    <br />
                                    <i class="fa fa-instagram" aria-hidden="true"></i>&nbsp;Instagram:
                                    <span class="text-primary">https://www.instagram.com/RestaurantE</span>
                                    <br />
                                </div>
                            </div>
                        </div>
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