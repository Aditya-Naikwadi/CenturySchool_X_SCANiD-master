<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true" CodeBehind="Infrastructure.aspx.cs" Inherits="CenturyRayonSchool.Infrastructure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,400i,600,700,700i&display=swap" rel="stylesheet" />
    <link href='https://fonts.googleapis.com/css2?family=Quintessential&display=swap' rel='stylesheet' />



    <link href="../css/fontawesome-all.css" rel="stylesheet" />


    <style>
        .head1 {
            font-size: xxx-large;
            font-weight: 600;
            margin-top: 115px;
            color: #74cee4;
            font-family: Quintessential;
            text-align: center;
        }

        h1 {
            color: #333;
            font: 700 2.75rem/3.375rem "Montserrat", sans-serif;
        }

        h3 {
            color: #333;
            font: 700 1.625rem/2.125rem "Montserrat", sans-serif;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="nicdark_section" style="background-color: #9d95951a;">
        <header id="header" class="header">
            <div class="head1">Infrastructure</div>
            <br />
        </header>
        

        <div class="container-fluid">

            <div class="card prayer-card" style="width: 70rem; margin: auto; border-width: 1px; border-style: ridge; border-color: #b2a787bd; border-radius: 25px; padding-left: 30px; padding-right: 30px">
                <div class="card-body">
                    <table class="table table-hover">
                        <tbody>
                            <tr>
                                <th scope="row" style="width:10%">Sr No.</th>
                                <th style="width:20%">Facility</th>
                                <th style="width:65%">Description</th>
                                <th style="width:5%">No.</th>
                            </tr>
                            <tr>
                              <td scope="row">1</td>
                              <td>IT enabled classrooms</td>
                              <td>Well ventilated, proper ambience for learning</td>
                              <td>39</td>
                            </tr>
                            <tr>
                              <td scope="row">2</td>
                              <td>Computer Lab</td>
                              <td>Students are introduced to computer science for primary and secondary section</td>
                              <td>2</td>
                            </tr>
                            <tr>
                              <td scope="row">3</td>
                              <td>Science Lab</td>
                              <td>Provides an open environment for students to experiment and do research work</td>
                              <td>2</td>
                            </tr>
                            <tr>
                              <td scope="row">4</td>
                              <td>E-Library</td>
                              <td>Vast collection of  E- books to provide access to a rich array of stories, ideas and information</td>
                              <td>1</td>
                            </tr>
                             <tr>
                              <td scope="row">5</td>
                              <td>Training  room</td>
                              <td>For presentations by staff, students and trainers comprehension and retention</td>
                              <td>1</td>
                            </tr>
                            <tr>
                              <td scope="row">7</td>
                              <td>Assembly hall</td>
                              <td>A vibrant place to provide wings to the thoughts of our students</td>
                              <td>1</td>
                            </tr>
                            <tr>
                              <td scope="row">8</td>
                              <td>Sports room & playgrounds</td>
                              <td>A wide plethora of indoor and outdoor games. Century Rayon  High  School, Shahad  conducts early morning and after school practice and coaching sessions for students in football, cricket, volleyball, Kabaddi,   kho-kho, chess, table tennis, Carrom and badminton., In recent years school team has bagged many prizes at inter school, district state and national level tournaments</td>
                              <td>2</td>
                            </tr>
                            <tr>
                              <td scope="row">9</td>
                              <td>Staffrooms</td>
                              <td>The teachers' lounge equipped with facilities like refrigerator, microwave to provide an ambience for effective planning</td>
                              <td>1</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="nicdark_space60"></div>
    </section>
</asp:Content>
