<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true"
    CodeBehind="SchoolFaculty.aspx.cs" Inherits="CenturyRayonSchool.SchoolFaculty" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="css/admin-modern.css" rel="stylesheet" />
        <link href="css/fontawesome-all.css" rel="stylesheet" />
        <style>
            .faculty-grid {
                display: grid;
                grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
                gap: 1.5rem;
                margin-top: 2rem;
            }

            .teacher-card {
                background: white;
                border-radius: 15px;
                padding: 1.5rem;
                box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
                transition: all 0.3s ease;
                text-align: center;
            }

            .teacher-card:hover {
                transform: translateY(-8px);
                box-shadow: 0 8px 25px rgba(124, 56, 72, 0.2);
            }

            .teacher-photo {
                width: 120px;
                height: 120px;
                border-radius: 50%;
                margin: 0 auto 1rem;
                border: 4px solid #7c3848;
                object-fit: cover;
            }

            .teacher-name {
                font-family: 'Poppins', sans-serif;
                font-size: 1.1rem;
                font-weight: 600;
                color: #2c3e50;
                margin-bottom: 0.5rem;
            }

            .teacher-qualification {
                font-size: 0.9rem;
                color: #6c757d;
                line-height: 1.6;
            }

            .section-header {
                background: linear-gradient(135deg, #7c3848 0%, #5a2a34 100%);
                color: white;
                padding: 2rem;
                border-radius: 15px;
                margin: 3rem 0 2rem;
                text-align: center;
            }

            .section-title {
                font-size: 2rem;
                font-weight: 700;
                margin: 0;
            }

            .section-count {
                font-size: 1.1rem;
                opacity: 0.9;
                margin-top: 0.5rem;
            }

            /* Toggle Button Styles */
            .faculty-toggle-container {
                display: flex;
                justify-content: center;
                gap: 1rem;
                margin: 2rem 0;
                flex-wrap: wrap;
            }

            .faculty-toggle-btn {
                background: white;
                border: 2px solid #7c3848;
                color: #7c3848;
                padding: 1rem 2rem;
                border-radius: 50px;
                font-size: 1.1rem;
                font-weight: 600;
                cursor: pointer;
                transition: all 0.3s ease;
                display: flex;
                align-items: center;
                gap: 0.5rem;
            }

            .faculty-toggle-btn:hover {
                background: #f5f3ee;
                transform: translateY(-2px);
                box-shadow: 0 4px 15px rgba(124, 56, 72, 0.2);
            }

            .faculty-toggle-btn.active {
                background: linear-gradient(135deg, #7c3848 0%, #5a2a34 100%);
                color: white;
                box-shadow: 0 8px 20px rgba(124, 56, 72, 0.3);
            }

            .faculty-toggle-btn .badge {
                background: rgba(255, 255, 255, 0.2);
                padding: 0.25rem 0.75rem;
                border-radius: 20px;
                font-size: 0.85rem;
            }

            .faculty-toggle-btn.active .badge {
                background: rgba(255, 255, 255, 0.3);
            }

            .faculty-section {
                display: none;
            }

            .faculty-section.active {
                display: block;
                animation: fadeIn 0.5s ease;
            }

            @keyframes fadeIn {
                from {
                    opacity: 0;
                    transform: translateY(20px);
                }

                to {
                    opacity: 1;
                    transform: translateY(0);
                }
            }

            @media (max-width: 768px) {
                .faculty-grid {
                    grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
                    gap: 1rem;
                }

                .faculty-toggle-btn {
                    padding: 0.75rem 1.5rem;
                    font-size: 1rem;
                }
            }

            .admin-modern-section {
                background: linear-gradient(135deg, #f5f7fa 0%, #c3cfe2 100%);
                min-height: 100vh;
                padding: 0 0 3rem;
                position: relative;
                overflow: hidden;
            }

            .admin-hero-header {
                text-align: center;
                padding: 1.5rem 1rem 1.5rem;
                position: relative;
                z-index: 1;
                margin-top: 70px;
            }

            .admin-hero-title {
                font-family: 'Poppins', sans-serif;
                font-size: clamp(2rem, 5vw, 3rem);
                font-weight: 700;
                background: linear-gradient(135deg, #2563eb 0%, #1e40af 100%);
                -webkit-background-clip: text;
                -webkit-text-fill-color: transparent;
                background-clip: text;
                margin-bottom: 0.5rem;
            }

            .admin-hero-subtitle {
                font-family: 'Inter', sans-serif;
                font-size: 1rem;
                color: #6c757d;
                font-weight: 400;
            }
        </style>
    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <section class="admin-modern-section">
            <div class="admin-hero-header">
                <h1 class="admin-hero-title">School Faculty</h1>
                <p class="admin-hero-subtitle">Meet Our Dedicated Team of Educators</p>
            </div>

            <div class="admin-container">
                <!-- Toggle Buttons -->
                <div class="faculty-toggle-container">
                    <button type="button" class="faculty-toggle-btn active" data-section="primary">
                        <i class="fas fa-chalkboard-teacher"></i> Primary Section
                        <span class="badge">32 Teachers</span>
                    </button>
                    <button type="button" class="faculty-toggle-btn" data-section="secondary">
                        <i class="fas fa-user-graduate"></i> Secondary Section
                        <span class="badge">67 Teachers</span>
                    </button>
                </div>
                <!-- PRIMARY SECTION -->
                <div id="primary-section" class="faculty-section active">
                    <div class="section-header">
                        <h2 class="section-title"><i class="fas fa-chalkboard-teacher"></i> Primary Section</h2>
                        <p class="section-count">32 Dedicated Teachers</p>
                    </div>

                    <div class="faculty-grid">
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Babita+Chanpawat&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Babita Gopal Chanpawat (Mrs.)</div>
                            <div class="teacher-qualification">M.A., D.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Neelam+Singh&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Neelam Santosh Singh (Mrs.)</div>
                            <div class="teacher-qualification">B.A., D.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Pravinkumar+Swami&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Pravinkumar S. Swami (Mr.)</div>
                            <div class="teacher-qualification">B.Sc., D.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Yojana+Sonawane&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Yojana D. Sonawane (Mrs.)</div>
                            <div class="teacher-qualification">B.A., D.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Puja+Singh&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Puja D. Singh (Mrs.)</div>
                            <div class="teacher-qualification">B.A., D.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Pushpa+Singh&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Pushpa Singh (Mrs.)</div>
                            <div class="teacher-qualification">H.S.C, D.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Sarita+Dubey&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Sarita S. Dubey (Mrs.)</div>
                            <div class="teacher-qualification">B.A., D.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Daksha+Singh&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Daksha Singh (Miss.)</div>
                            <div class="teacher-qualification">H.S.C., D.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Nitin+Singh&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Nitin Singh (Mr.)</div>
                            <div class="teacher-qualification">H.S.C., D.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Pooja+Chaubey&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Pooja Pawan Chaubey (Mrs.)</div>
                            <div class="teacher-qualification">M.A., D.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Pratima+Singh&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Pratima R. Singh (Mrs.)</div>
                            <div class="teacher-qualification">H.S.C, D.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Durgeshkumar+Yadav&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Durgeshkumar M. Yadav (Mr.)</div>
                            <div class="teacher-qualification">B.A., B.Ed., D.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Supriya+Patil&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Supriya Subodh Patil (Mrs.)</div>
                            <div class="teacher-qualification">B.A., D.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Sarita+Yadav&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Sarita Vasudev Yadav (Mrs.)</div>
                            <div class="teacher-qualification">B.A., M.A, D.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Parvatamma+Boya&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Parvatamma S. Boya (Miss.)</div>
                            <div class="teacher-qualification">H.S.C, D.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Deepak+Sarnobat&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Deepak Shankar Sarnobat (Mr.)</div>
                            <div class="teacher-qualification">B.A., D.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Nalini+Jadhav&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Nalini Yashwant Jadhav (Mrs.)</div>
                            <div class="teacher-qualification">M.A., D.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Archana+Waghmare&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Archana C. Waghmare (Mrs.)</div>
                            <div class="teacher-qualification">B.A., D.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Suvarna+Dahiphale&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Suvarna S. Dahiphale (Mrs.)</div>
                            <div class="teacher-qualification">B.A., D.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Mrudula+Peshwe&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Mrudula R. Peshwe (Mrs.)</div>
                            <div class="teacher-qualification">B.A., D.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Aarti+Patil&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Aarti Anandrao Patil (Mrs.)</div>
                            <div class="teacher-qualification">B.A., D.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Seena+Bodakhe&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Seena Pravin Bodakhe (Mrs.)</div>
                            <div class="teacher-qualification">B.A., D.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Pratibha+Patil&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Pratibha J. Patil (Mrs.)</div>
                            <div class="teacher-qualification">B.A., D.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Bhalchandra+Jadhav&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Bhalchandra Jadhav (Mr.)</div>
                            <div class="teacher-qualification">B.A.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Santosh+Kanoje&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Santosh M. Kanoje (Mr.)</div>
                            <div class="teacher-qualification">B.A., D.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Indutai+Ingle&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Indutai D. Ingle (Mrs.)</div>
                            <div class="teacher-qualification">B.A., D.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Manoj+Bhurkunde&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Manoj P. Bhurkunde (Mr.)</div>
                            <div class="teacher-qualification">B.A., D.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Leena+Goyal&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Leena Santosh Goyal (Miss.)</div>
                            <div class="teacher-qualification">B.Com, D.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Savita+Agrawal&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Savita Agrawal (Mrs.)</div>
                            <div class="teacher-qualification">B.Com., D.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Firdous+Shaikh&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Firdous Shaikh (Miss.)</div>
                            <div class="teacher-qualification">B.A., B.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Sunil+Daga&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Sunil J. Daga (Mr.)</div>
                            <div class="teacher-qualification">Yog Adhyapak/YCB Level 2</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Supriya+Khachane&background=7c3848&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Supriya Khachane (Mrs.)</div>
                            <div class="teacher-qualification">A.T.D. Dea.P.Ad.</div>
                        </div>
                    </div>
                </div>

                <!-- SECONDARY SECTION -->
                <div id="secondary-section" class="faculty-section">
                    <div class="section-header">
                        <h2 class="section-title"><i class="fas fa-user-graduate"></i> Secondary Section</h2>
                        <p class="section-count">67 Dedicated Teachers</p>
                    </div>

                    <div class="faculty-grid">
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Vandana+Bhadane&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Vandana Yuvraj Bhadane (Mrs.)</div>
                            <div class="teacher-qualification">B.Sc, B.Ed, M.A.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Dattatray+Dudhavade&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Dattatray S. Dudhavade (Mr.)</div>
                            <div class="teacher-qualification">B.A., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Vidyarani+Gaikwad&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Vidyarani Vikas Gaikwad (Mrs.)</div>
                            <div class="teacher-qualification">M.A., M.Ed., D.S.M.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Pramod+Parsi&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Pramod M. Parsi (Mr.)</div>
                            <div class="teacher-qualification">B.Sc, M.P.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Seeta+Sahu&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Seeta Aswinikumar Sahu (Mrs.)</div>
                            <div class="teacher-qualification">M.A., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Anita+Chaudhari&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Anita Rajesh Chaudhari (Mrs.)</div>
                            <div class="teacher-qualification">B.A., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Maninath+Sagbhor&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Maninath H. Sagbhor (Mr.)</div>
                            <div class="teacher-qualification">M.A., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Rajendra+Kale&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Rajendra Ramdas Kale (Mr.)</div>
                            <div class="teacher-qualification">B.A., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Sangeeta+Thakre&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Sangeeta Thakre (Mrs.)</div>
                            <div class="teacher-qualification">ATD, B.A., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Kishor+Patil&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Kishor Dnyaneshwar Patil (Mr.)</div>
                            <div class="teacher-qualification">M.Sc, B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Deepak+Laycha&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Deepak M. Laycha (Mr.)</div>
                            <div class="teacher-qualification">M.A., M.Com, B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Dhirendra+Singh&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Dhirendra Pratap Singh (Mr.)</div>
                            <div class="teacher-qualification">M.Sc, B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Pratiksha+Bawa&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Pratiksha V. Bawa (Mrs.)</div>
                            <div class="teacher-qualification">B.A., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Jayshree+Khedkar&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Jayshree P. Khedkar (Mrs.)</div>
                            <div class="teacher-qualification">B.A., M.A, D.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Manisha+Ahire&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Manisha S. Ahire (Mrs.)</div>
                            <div class="teacher-qualification">M.A., D.Ed, B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Dattatray+Shene&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Dattatray Vaman Shene (Mr.)</div>
                            <div class="teacher-qualification">M.A., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Motilal+Jadhav&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Motilal Meharban Jadhav (Mr.)</div>
                            <div class="teacher-qualification">M.A., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Reshme+Nair&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Reshme Prem Nair (Mrs.)</div>
                            <div class="teacher-qualification">B.A., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Megha+Bhawar&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Megha Mahendra Bhawar (Mrs.)</div>
                            <div class="teacher-qualification">H.S.C., D.Ed, B.A</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Megha+Ingole&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Megha Pramod Ingole (Mrs.)</div>
                            <div class="teacher-qualification">M.A., D.Ed, B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Neelam+Pandey&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Neelam Rakeshk. Pandey (Mrs.)</div>
                            <div class="teacher-qualification">M.A., D.Ed., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Manisha+Chavan&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Manisha Suresh Chavan (Mrs.)</div>
                            <div class="teacher-qualification">D.Ed, M.A., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Somnath+Shene&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Somnath Vaman Shene (Mr.)</div>
                            <div class="teacher-qualification">M.A., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Vaishali+Godambe&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Vaishali S. Godambe (Mrs.)</div>
                            <div class="teacher-qualification">ATD, A.M.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Ashutosh+Tripathi&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Ashutosh Tripathi (Mr.)</div>
                            <div class="teacher-qualification">M.A., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Anchala+Rai&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Anchala A. Rai (Mrs.)</div>
                            <div class="teacher-qualification">M.Sc, B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Arvind+Pandey&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Arvind A. Pandey (Mr.)</div>
                            <div class="teacher-qualification">M.A., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Bharati+Baisane&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Bharati H. Baisane (Mrs.)</div>
                            <div class="teacher-qualification">M.A., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Vijayalaxmi+Mehetre&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Vijayalaxmi Mehetre (Mrs.)</div>
                            <div class="teacher-qualification">B.A., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Sushma+Pandey&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Sushma D. Pandey (Mrs.)</div>
                            <div class="teacher-qualification">M.A., D.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Harish+Dhongade&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Harish V. Dhongade (Mr.)</div>
                            <div class="teacher-qualification">B.A., D.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Triloknath+Jaiswar&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Triloknath Baljit Jaiswar (Mr.)</div>
                            <div class="teacher-qualification">B.A., D.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Kalpana+Mali&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Kalpana Kailas Mali (Mrs.)</div>
                            <div class="teacher-qualification">B.A., D.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Megha+Pawar&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Megha Ganesh Pawar (Mrs.)</div>
                            <div class="teacher-qualification">B.A., D.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Aarti+Sarnobat&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Aarti Deepak Sarnobat (Mrs.)</div>
                            <div class="teacher-qualification">H.S.C, D.Ed, M.A</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Prafull+Adgale&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Prafull Daulat Adgale (Mr.)</div>
                            <div class="teacher-qualification">B.A., B.P.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Rakeshkumar+Pandey&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Rakeshkumar J. Pandey (Mr.)</div>
                            <div class="teacher-qualification">B.Sc, B.Ed, M.A</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Pranita+Gurav&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Pranita Prakash Gurav (Mrs.)</div>
                            <div class="teacher-qualification">B.A., D.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Vithal+Mange&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Vithal Lahu Mange (Mr.)</div>
                            <div class="teacher-qualification">M.A., D.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Santosh+Sarogade&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Santosh Hari Sarogade (Mr.)</div>
                            <div class="teacher-qualification">B.A., H.S.C, D.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Santosh+Thakare&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Santosh T. Thakare (Mr.)</div>
                            <div class="teacher-qualification">B.A., H.S.C, D.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Bhagvat+Mahale&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Bhagvat U. Mahale (Mr.)</div>
                            <div class="teacher-qualification">M.A., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Yogesh+Jagale&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Yogesh Ashok Jagale (Mr.)</div>
                            <div class="teacher-qualification">B.A., B.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Kanhaiya+Pagare&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Kanhaiya Pramod Pagare (Mr.)</div>
                            <div class="teacher-qualification">B.Sc, B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Akhilesh+Mishra&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Akhilesh V. Mishra (Mr.)</div>
                            <div class="teacher-qualification">B.Sc, B.Ed, M.Ed, M.A.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Pradatt+Upadhyay&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Pradatt B. Upadhyay (Mr.)</div>
                            <div class="teacher-qualification">B.Sc, B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Vinodkumar+Mishra&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Vinodkumar R. Mishra (Mr.)</div>
                            <div class="teacher-qualification">B.A., D.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Yogesh+Kurpane&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Yogesh Kurpane (Mr.)</div>
                            <div class="teacher-qualification">M.A., A.M.G.D, Art</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Vaishali+Upadhyay&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Vaishali S. Upadhyay (Mrs.)</div>
                            <div class="teacher-qualification">M.Sc., B.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Shivani+Singh&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Shivani A. Singh (Mrs.)</div>
                            <div class="teacher-qualification">D.Ed, B.Sc</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Rekha+Issrani&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Rekha Ramchand Issrani (Miss)</div>
                            <div class="teacher-qualification">M.A., D.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Snehal+Ghodke&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Snehal Ravindra Ghodke (Miss)</div>
                            <div class="teacher-qualification">M.Sc (IT), B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Janhavi+Bhoir&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Janhavi Eknath Bhoir (Mrs.)</div>
                            <div class="teacher-qualification">B.A., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Archana+Rai&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Archana Rajesh Rai (Mrs.)</div>
                            <div class="teacher-qualification">M.A., M.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Ghanshyam+Vishwakarma&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Ghanshyam Vishwakarma (Mr.)</div>
                            <div class="teacher-qualification">B.Sc, B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Mamata+Pathak&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Mamata O. Pathak (Mrs.)</div>
                            <div class="teacher-qualification">M.A., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Subhash+Kondhari&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Subhash Ramji Kondhari (Mr.)</div>
                            <div class="teacher-qualification">B.A., B.P.Ed, M.P.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Alka+Sarak&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Alka Pandurang Sarak (Mrs.)</div>
                            <div class="teacher-qualification">B.A., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Kailas+Tarmale&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Kailas M. Tarmale (Mr.)</div>
                            <div class="teacher-qualification">B.A., D.Ed.</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Rajendra+Patil&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Rajendra Atmaram Patil (Mr.)</div>
                            <div class="teacher-qualification">M.A., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Pradeep+Bendar&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Pradeep Chaitya Bendar (Mr.)</div>
                            <div class="teacher-qualification">B.Sc, B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Kumud+Singh&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Kumud M. Singh (Mrs.)</div>
                            <div class="teacher-qualification">M.A., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Sanjay+Mali&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Sanjay R. Mali (Mr.)</div>
                            <div class="teacher-qualification">B.A., B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Mahendra+Patil&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Mahendra Dashrath Patil (Mr.)</div>
                            <div class="teacher-qualification">ATD, G.D.Art, A.M</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Mamta+Pandit&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Mamta Umesh Pandit (Mrs.)</div>
                            <div class="teacher-qualification">H.S.C, D.Ed, B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Mamta+Dubey&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Mamta Pramesh Dubey (Mrs.)</div>
                            <div class="teacher-qualification">M.Sc, B.Ed</div>
                        </div>
                        <div class="teacher-card"><img
                                src="https://ui-avatars.com/api/?name=Mamta+Dubey&background=2563eb&color=fff&size=150"
                                class="teacher-photo" alt="Teacher">
                            <div class="teacher-name">Mamta Dubey (Mrs.)</div>
                            <div class="teacher-qualification">M.Sc., B.Ed</div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <!-- Toggle JavaScript -->
        <script>
            document.addEventListener('DOMContentLoaded', function () {
                const toggleBtns = document.querySelectorAll('.faculty-toggle-btn');

                toggleBtns.forEach(btn => {
                    btn.addEventListener('click', function () {
                        const section = this.dataset.section;

                        // Update button states
                        toggleBtns.forEach(b => b.classList.remove('active'));
                        this.classList.add('active');

                        // Show/hide sections with smooth transition
                        document.querySelectorAll('.faculty-section').forEach(s => {
                            s.classList.remove('active');
                        });
                        document.getElementById(section + '-section').classList.add('active');

                        // Smooth scroll to top of toggle container
                        document.querySelector('.faculty-toggle-container').scrollIntoView({
                            behavior: 'smooth',
                            block: 'nearest'
                        });
                    });
                });
            });
        </script>
    </asp:Content>