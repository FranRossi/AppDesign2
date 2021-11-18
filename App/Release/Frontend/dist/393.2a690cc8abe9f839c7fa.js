(self.webpackChunkBugSummaryFrontend=self.webpackChunkBugSummaryFrontend||[]).push([[393],{1393:function(e,n,t){"use strict";t.r(n),t.d(n,{AuthLayoutModule:function(){return v}});var r=t(738),o=t(1116),i=t(6728),s=t(529),a=t(3820),u=t(2693),g=function(){function e(e){this.http=e,this.endpoint=s.N.webApi_origin+"/sessions"}return e.prototype.loginUser=function(e){return this.http.post(this.endpoint,e)},e.\u0275fac=function(n){return new(n||e)(a.LFG(u.eN))},e.\u0275prov=a.Yz7({token:e,factory:e.\u0275fac,providedIn:"root"}),e}(),d=t(9821),c=["formSignIn"];function p(e,n){1&e&&(a.TgZ(0,"span",32),a._uU(1,"Please enter a username!"),a.qZA())}function l(e,n){1&e&&(a.TgZ(0,"span",32),a._uU(1,"Please enter a password!"),a.qZA())}function Z(e,n){if(1&e&&(a.TgZ(0,"div",33),a._uU(1),a.qZA()),2&e){var t=a.oxw();a.xp6(1),a.hij(" ",t.error," ")}}var f=[{path:"login",component:function(){function e(e,n,t){this.http=e,this.loginService=n,this.router=t,this.receivedToken=!1,this.error=null}return e.prototype.ngOnInit=function(){},e.prototype.ngOnDestroy=function(){},e.prototype.onSignIn=function(){var e=this;this.loginService.loginUser(this.signInForm.value).subscribe({next:function(n){sessionStorage.setItem("userToken",n.token),sessionStorage.setItem("userRole",n.role.toString()),e.receivedToken=!0,e.loadDashboard()},error:function(n){e.error=d.q.onHandleError(n)}})},e.prototype.loadDashboard=function(){if(this.receivedToken){var e=this.convertRoleNumberToString();this.router.navigate([e],{replaceUrl:!0})}},e.prototype.convertRoleNumberToString=function(){var e=sessionStorage.getItem("userRole"),n="3"===e?"admin":"2"===e?"developer":"tester";return sessionStorage.setItem("roleName",n),n},e.\u0275fac=function(n){return new(n||e)(a.Y36(u.eN),a.Y36(g),a.Y36(r.F0))},e.\u0275cmp=a.Xpm({type:e,selectors:[["app-login"]],viewQuery:function(e,n){var t;1&e&&a.Gf(c,5),2&e&&a.iGM(t=a.CRH())&&(n.signInForm=t.first)},decls:43,vars:4,consts:[[1,"header","bg-gradient-danger","py-7","py-lg-8"],[1,"container"],[1,"header-body","text-center","mb-7"],[1,"row","justify-content-center"],[1,"col-lg-5","col-md-6"],[1,"text-white"],[1,"text-lead","text-light"],[1,"separator","separator-bottom","separator-skew","zindex-100"],["x","0","y","0","viewBox","0 0 2560 100","preserveAspectRatio","none","xmlns","http://www.w3.org/2000/svg"],["points","2560 0 2560 100 0 100",1,"fill-default"],[1,"container","mt--8","pb-5"],[1,"col-lg-5","col-md-7"],[1,"card","bg-secondary","shadow","border-0"],[1,"card-body","px-lg-5","py-lg-5"],[1,"text-center","text-muted","mb-4"],[3,"ngSubmit"],["formSignIn","ngForm"],[1,"form-group","mb-3"],[1,"input-group","input-group-alternative"],[1,"input-group-prepend"],[1,"input-group-text"],[1,"ni","ni-single-02"],["placeholder","Username","name","username","type","text","ngModel","","required","",1,"form-control"],["username","ngModel"],["class","text-sm",4,"ngIf"],[1,"form-group"],[1,"ni","ni-lock-circle-open"],["placeholder","Password","name","password","type","password","ngModel","","required","",1,"form-control"],["pass","ngModel"],[1,"text-center"],["type","submit",1,"btn","btn-primary","my-4",3,"disabled"],["class","alert alert-danger","role","alert",4,"ngIf"],[1,"text-sm"],["role","alert",1,"alert","alert-danger"]],template:function(e,n){if(1&e&&(a.TgZ(0,"div",0),a.TgZ(1,"div",1),a.TgZ(2,"div",2),a.TgZ(3,"div",3),a.TgZ(4,"div",4),a.TgZ(5,"h1",5),a._uU(6,"Welcome!"),a.qZA(),a.TgZ(7,"p",6),a._uU(8,"Please enter your credentials to start using BugSummary."),a.qZA(),a.qZA(),a.qZA(),a.qZA(),a.qZA(),a.TgZ(9,"div",7),a.O4$(),a.TgZ(10,"svg",8),a._UZ(11,"polygon",9),a.qZA(),a.qZA(),a.qZA(),a.kcU(),a.TgZ(12,"div",10),a.TgZ(13,"div",3),a.TgZ(14,"div",11),a.TgZ(15,"div",12),a.TgZ(16,"div",13),a.TgZ(17,"div",14),a.TgZ(18,"small"),a._uU(19,"Sign in with credentials"),a.qZA(),a.qZA(),a.TgZ(20,"form",15,16),a.NdJ("ngSubmit",function(){return n.onSignIn()}),a.TgZ(22,"div",17),a.TgZ(23,"div",18),a.TgZ(24,"div",19),a.TgZ(25,"span",20),a._UZ(26,"i",21),a.qZA(),a.qZA(),a._UZ(27,"input",22,23),a.qZA(),a.YNc(29,p,2,0,"span",24),a.qZA(),a.TgZ(30,"div",25),a.TgZ(31,"div",18),a.TgZ(32,"div",19),a.TgZ(33,"span",20),a._UZ(34,"i",26),a.qZA(),a.qZA(),a._UZ(35,"input",27,28),a.qZA(),a.YNc(37,l,2,0,"span",24),a.qZA(),a.TgZ(38,"div",29),a.TgZ(39,"button",30),a._uU(40," Sign in "),a.qZA(),a.qZA(),a.qZA(),a.TgZ(41,"div",29),a.YNc(42,Z,2,1,"div",31),a.qZA(),a.qZA(),a.qZA(),a.qZA(),a.qZA(),a.qZA()),2&e){var t=a.MAs(21),r=a.MAs(28),o=a.MAs(36);a.xp6(29),a.Q6J("ngIf",!r.valid&&r.touched),a.xp6(8),a.Q6J("ngIf",!o.valid&&o.touched),a.xp6(2),a.Q6J("disabled",!t.valid),a.xp6(3),a.Q6J("ngIf",n.error)}},directives:[i._Y,i.JL,i.F,i.Fj,i.JJ,i.On,i.Q7,o.O5],styles:["input.ng-invalid.ng-touched[_ngcontent-%COMP%] {\n  border: 1px solid indianred;\n}"]}),e}()}],v=function(){function e(){}return e.\u0275fac=function(n){return new(n||e)},e.\u0275mod=a.oAB({type:e}),e.\u0275inj=a.cJS({imports:[[o.ez,r.Bz.forChild(f),i.u5]]}),e}()},9821:function(e,n,t){"use strict";t.d(n,{q:function(){return r}});var r=function(){function e(){}return e.onHandleError=function(e){return 0===e.statusCode||null!=e&&e.error instanceof ProgressEvent?"Cannot connect to WebApi":e.error},e}()}}]);