<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Sinav_Error" %>


<div class="col-12">
            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div class="col-12 center-block">
                            <img src="/CevrimiciSinav/Content/images/error.png" alt="Hata" class="rounded mx-auto d-block" />

                        </div>
                    </div>
                    <p class="mt-2 login-box-msg">
                        Bir hata oldu. Sınava kaldığın yerden devam edebilirsiz. 
                    </p> <p class="mt-2 login-box-msg">
                        Sınavlar butonuna tıklayarak bu testi yeniden başlatabilirsiniz.
                    </p>
                    <div class="row">
                        <div class="col-4 rounded mx-auto d-block">
                            <a href="Sinavlar.aspx" id="error" class="btn btn-success btn-block">Sınavlar</a>
                        </div> 
                    </div>

                </div>
            </div>
        </div>
