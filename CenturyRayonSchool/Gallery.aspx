<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true" CodeFile="Gallery.aspx.cs" Inherits="CenturyRayonSchool.Photogallery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href='https://fonts.googleapis.com/css2?family=Quintessential&display=swap' rel='stylesheet'/>
    <link rel="stylesheet" href="css/photogallery.css" />
    <script type="text/javascript" src="js/plugins.js"></script>
    <script type="text/javascript" src="js/photogallery.js"></script>
    <style>
        .head1{
            font-size:xxx-large;
    font-weight: 600;
    margin-top: 115px;
    color:#74cee4;
    font-family:Quintessential;
    text-align:center;

        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section class="nicdark_section" style="background-color:#9d95951a">
      <header id="header" class="header" >
     
        <div class="head1">School Photo Gallery</div><br />
    </header> 
    
     <div style="margin-top:50px;">
        <div class="container-fluid">
           
               <div class="portfolio gallery-section">
            <div class="container">
                <div class="isotope-filters portfolio-filter" style="font-size:larger;font-weight:bolder;">
                    <button type="button" class="button is-checked" data-sort-by="original-order" onclick="setGalleryDesc('')">ALL</button>
                    <%=gallerynames %>
                    
                </div>
                <div style="margin-bottom:15px;">
                    <asp:Label Text="" runat="server" ID="lblgaldescp" style="color:maroon;" />
                </div>
                <!-- end isotope filer -->
            </div>
            <!-- end container -->

            <div class="container-fluid clearfix imggallery portfolio-item isotope-items isotope-masonry-items">

                <%=galleryimages %>
             
              

            </div>
            <!-- /.gallery-item -->
            <%--<a class="read-more" href="#">View More</a>--%>
            <div class="clearfix"></div>
        </div>
           
        </div>
    </div>
          </section>
      <div id="myModal" class="modal">
            <span class="close" id="close">&times;</span>
            <img class="modal-content" id="img01">
            <div id="caption"></div>
        </div>

    <script>
        $(document).ready(function () {
            $('.imggallery').magnificPopup({
                type: 'image',
                closeOnContentClick: true,
                delegate: '.img-magnify',
                gallery: {
                    enabled: true,
                }
            });
        });

        function setGalleryDesc(gdescp) {
            $("#ContentPlaceHolder1_lblgaldescp").text(gdescp);

        }



    </script>


</asp:Content>
