<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Aboutus.aspx.cs" Inherits="Aboutus" %>
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
    <title>Sobre nós ~ Restaurant-e</title>
</head>
<body>
    <form id="form1" runat="server" style="height: 100vh;">
        <div class="container h-100">
            <div class="row">
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
                            <li class="btn btn-primary focus py-0 mx-md-2 my-2 my-md-0">
                                <span class="nav-link text-white active"> Sobre nós </span>
                            </li>
                            <li class="btn btn-primary py-0">
                                <a class="nav-link text-white" href="/Contact.aspx"> Contato </a>
                            </li>
                        </ul>
                    </div>
                </nav>
                <%-- ~~~~~~~~~~~~~~~~~~~~~~~~ Home - aboutus ~~~~~~~~~~~~~~~~~~~~~~~~ --%>

                <%-- ~~~~~~~~~~~~~~~~~~~~ Israel Gonçalves ~~~~~~~~~~~~~~~~~~~~ --%>
                <div class="col-12 col-md-6 pt-5 mt-5">
                    <div class="card bg-primary px-1 mx-0">
                        <div class="card-header text-center">
                            <img src="/images/israel.jpg" alt="404" class="rounded-circle" width="200px" height="200px"/>
                        </div>
                        <div class="card-body text-center">
                            <label> Israel Gonçalves </label>
                            <div class="row">
                                <div class="col-12">
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%-- ~~~~~~~~~~~~~~~~~~~~ Lojan Eduardo ~~~~~~~~~~~~~~~~~~~~ --%>
                <div class="col-12 col-md-6 pt-0 pt-md-5 mt-md-5 mt-2">
                    <div class="card bg-primary px-1 mx-0">
                        <div class="card-header text-center">
                            <img src="/images/lojan.jpg" alt="404" class="rounded-circle" width="200px" height="200px"/>
                        </div>
                        <div class="card-body text-center">
                            <label> Lojan Eduardo </label>
                            <div class="row">
                                <div class="col-12">
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%-- ~~~~~~~~~~~~~~~~~~~~ João Igor ~~~~~~~~~~~~~~~~~~~~ --%>
                <div class="col-12 col-md-6 mt-2">
                    <div class="card bg-primary px-1 mx-0">
                        <div class="card-header text-center">
                            <img src="/images/joao.jpg" alt="404" class="rounded-circle" width="200px" height="200px"/>
                        </div>
                        <div class="card-body text-center">
                            <label> João Igor </label>
                            <div class="row">
                                <div class="col-12">
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%-- ~~~~~~~~~~~~~~~~~~~~ Victor Rivera ~~~~~~~~~~~~~~~~~~~~ --%>
                <div class="col-12 col-md-6 mt-2">
                    <div class="card bg-primary px-1 mx-0">
                        <div class="card-header text-center">
                            <img src="/images/rivera.jpg" alt="404" class="rounded-circle" width="200px" height="200px"/>
                        </div>
                        <div class="card-body text-center">
                            <label> Victor Rivera </label>
                            <div class="row">
                                <div class="col-12">
                                    
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