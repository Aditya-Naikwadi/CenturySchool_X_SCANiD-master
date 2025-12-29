<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true" CodeBehind="VideoGallery.aspx.cs" Inherits="CenturyRayonSchool.VideoGallery" %>
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
        <!--  <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12">
                   
                </div> 
            </div> 
        </div>  -->
        <div class="head1">School Video Gallery</div><br />
    </header>
         <div class="container-fluid">
           
               <div class="portfolio gallery-section">
            <div class="container" style="margin-top:50px">
                <div class="isotope-filters portfolio-filter" style="font-size:larger;font-weight:bolder;">
                    <button class="button is-checked" data-sort-by="original-order">ALL</button>
                   

                    <%=gallerynames %>
                    
                </div>
                <!-- end isotope filer -->
            </div>
            <!-- end container -->

            <div class="container-fluid clearfix imggallery portfolio-item isotope-items isotope-masonry-items">

                <%=galleryimages %>
             
            <div class="clearfix"></div>
        </div>
           
        </div>
              <div id="myModal" class="modal">
            <span class="close" id="close">&times;</span>
          <video class="modal-content" id="img01" />
            <div id="caption"></div>
        </div>

         </section>

     <script>
        $(document).ready(function () {
            $('.imggallery').magnificPopup({
                type: 'video',
                closeOnContentClick: true,
                delegate: '.img-magnify',
                gallery: {
                    enabled: true,
                }
            });
        });



     </script>
</asp:Content>
