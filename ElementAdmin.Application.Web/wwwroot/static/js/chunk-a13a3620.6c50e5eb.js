(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-a13a3620"],{2201:function(e,t,a){"use strict";var i=a("d23e"),s=a.n(i);s.a},"636d":function(e,t,a){"use strict";var i=a("ad09"),s=a.n(i);s.a},7913:function(e,t,a){"use strict";a.r(t);var i=function(){var e=this,t=e.$createElement,a=e._self._c||t;return a("div",{staticClass:"components-container"},[a("aside",[e._v("This is based on\n    "),a("a",{staticClass:"link-type",attrs:{href:"//github.com/dai-siki/vue-image-crop-upload"}},[e._v(" vue-image-crop-upload")]),e._v(".\n    "+e._s(e.$t("components.imageUploadTips"))+"\n  ")]),e._v(" "),a("pan-thumb",{attrs:{image:e.image}}),e._v(" "),a("el-button",{staticStyle:{position:"absolute",bottom:"15px","margin-left":"40px"},attrs:{type:"primary",icon:"upload"},on:{click:function(t){e.imagecropperShow=!0}}},[e._v("\n    Change Avatar\n  ")]),e._v(" "),a("image-cropper",{directives:[{name:"show",rawName:"v-show",value:e.imagecropperShow,expression:"imagecropperShow"}],key:e.imagecropperKey,attrs:{width:300,height:300,url:"https://httpbin.org/post","lang-type":"en"},on:{close:e.close,"crop-upload-success":e.cropSuccess}})],1)},s=[],o=function(){var e=this,t=e.$createElement,a=e._self._c||t;return a("div",{directives:[{name:"show",rawName:"v-show",value:e.value,expression:"value"}],staticClass:"vue-image-crop-upload"},[a("div",{staticClass:"vicp-wrap"},[a("div",{staticClass:"vicp-close",on:{click:e.off}},[a("i",{staticClass:"vicp-icon4"})]),e._v(" "),a("div",{directives:[{name:"show",rawName:"v-show",value:1==e.step,expression:"step == 1"}],staticClass:"vicp-step1"},[a("div",{staticClass:"vicp-drop-area",on:{dragleave:e.preventDefault,dragover:e.preventDefault,dragenter:e.preventDefault,click:e.handleClick,drop:e.handleChange}},[a("i",{directives:[{name:"show",rawName:"v-show",value:1!=e.loading,expression:"loading != 1"}],staticClass:"vicp-icon1"},[a("i",{staticClass:"vicp-icon1-arrow"}),e._v(" "),a("i",{staticClass:"vicp-icon1-body"}),e._v(" "),a("i",{staticClass:"vicp-icon1-bottom"})]),e._v(" "),a("span",{directives:[{name:"show",rawName:"v-show",value:1!==e.loading,expression:"loading !== 1"}],staticClass:"vicp-hint"},[e._v(e._s(e.lang.hint))]),e._v(" "),a("span",{directives:[{name:"show",rawName:"v-show",value:!e.isSupported,expression:"!isSupported"}],staticClass:"vicp-no-supported-hint"},[e._v(e._s(e.lang.noSupported))]),e._v(" "),1==e.step?a("input",{directives:[{name:"show",rawName:"v-show",value:!1,expression:"false"}],ref:"fileinput",attrs:{type:"file"},on:{change:e.handleChange}}):e._e()]),e._v(" "),a("div",{directives:[{name:"show",rawName:"v-show",value:e.hasError,expression:"hasError"}],staticClass:"vicp-error"},[a("i",{staticClass:"vicp-icon2"}),e._v(" "+e._s(e.errorMsg)+"\n      ")]),e._v(" "),a("div",{staticClass:"vicp-operate"},[a("a",{on:{click:e.off,mousedown:e.ripple}},[e._v(e._s(e.lang.btn.off))])])]),e._v(" "),2==e.step?a("div",{staticClass:"vicp-step2"},[a("div",{staticClass:"vicp-crop"},[a("div",{directives:[{name:"show",rawName:"v-show",value:!0,expression:"true"}],staticClass:"vicp-crop-left"},[a("div",{staticClass:"vicp-img-container"},[a("img",{ref:"img",staticClass:"vicp-img",style:e.sourceImgStyle,attrs:{src:e.sourceImgUrl,draggable:"false"},on:{drag:e.preventDefault,dragstart:e.preventDefault,dragend:e.preventDefault,dragleave:e.preventDefault,dragover:e.preventDefault,dragenter:e.preventDefault,drop:e.preventDefault,touchstart:e.imgStartMove,touchmove:e.imgMove,touchend:e.createImg,touchcancel:e.createImg,mousedown:e.imgStartMove,mousemove:e.imgMove,mouseup:e.createImg,mouseout:e.createImg}}),e._v(" "),a("div",{staticClass:"vicp-img-shade vicp-img-shade-1",style:e.sourceImgShadeStyle}),e._v(" "),a("div",{staticClass:"vicp-img-shade vicp-img-shade-2",style:e.sourceImgShadeStyle})]),e._v(" "),a("div",{staticClass:"vicp-range"},[a("input",{attrs:{type:"range",step:"1",min:"0",max:"100"},domProps:{value:e.scale.range},on:{input:e.zoomChange}}),e._v(" "),a("i",{staticClass:"vicp-icon5",on:{mousedown:e.startZoomSub,mouseout:e.endZoomSub,mouseup:e.endZoomSub}}),e._v(" "),a("i",{staticClass:"vicp-icon6",on:{mousedown:e.startZoomAdd,mouseout:e.endZoomAdd,mouseup:e.endZoomAdd}})]),e._v(" "),e.noRotate?e._e():a("div",{staticClass:"vicp-rotate"},[a("i",{on:{mousedown:e.startRotateLeft,mouseout:e.endRotate,mouseup:e.endRotate}},[e._v("↺")]),e._v(" "),a("i",{on:{mousedown:e.startRotateRight,mouseout:e.endRotate,mouseup:e.endRotate}},[e._v("↻")])])]),e._v(" "),a("div",{directives:[{name:"show",rawName:"v-show",value:!0,expression:"true"}],staticClass:"vicp-crop-right"},[a("div",{staticClass:"vicp-preview"},[e.noSquare?e._e():a("div",{staticClass:"vicp-preview-item"},[a("img",{style:e.previewStyle,attrs:{src:e.createImgUrl}}),e._v(" "),a("span",[e._v(e._s(e.lang.preview))])]),e._v(" "),e.noCircle?e._e():a("div",{staticClass:"vicp-preview-item vicp-preview-item-circle"},[a("img",{style:e.previewStyle,attrs:{src:e.createImgUrl}}),e._v(" "),a("span",[e._v(e._s(e.lang.preview))])])])])]),e._v(" "),a("div",{staticClass:"vicp-operate"},[a("a",{on:{click:function(t){return e.setStep(1)},mousedown:e.ripple}},[e._v(e._s(e.lang.btn.back))]),e._v(" "),a("a",{staticClass:"vicp-operate-btn",on:{click:e.prepareUpload,mousedown:e.ripple}},[e._v(e._s(e.lang.btn.save))])])]):e._e(),e._v(" "),3==e.step?a("div",{staticClass:"vicp-step3"},[a("div",{staticClass:"vicp-upload"},[a("span",{directives:[{name:"show",rawName:"v-show",value:1===e.loading,expression:"loading === 1"}],staticClass:"vicp-loading"},[e._v(e._s(e.lang.loading))]),e._v(" "),a("div",{staticClass:"vicp-progress-wrap"},[a("span",{directives:[{name:"show",rawName:"v-show",value:1===e.loading,expression:"loading === 1"}],staticClass:"vicp-progress",style:e.progressStyle})]),e._v(" "),a("div",{directives:[{name:"show",rawName:"v-show",value:e.hasError,expression:"hasError"}],staticClass:"vicp-error"},[a("i",{staticClass:"vicp-icon2"}),e._v(" "+e._s(e.errorMsg)+"\n        ")]),e._v(" "),a("div",{directives:[{name:"show",rawName:"v-show",value:2===e.loading,expression:"loading === 2"}],staticClass:"vicp-success"},[a("i",{staticClass:"vicp-icon3"}),e._v(" "+e._s(e.lang.success)+"\n        ")])]),e._v(" "),a("div",{staticClass:"vicp-operate"},[a("a",{on:{click:function(t){return e.setStep(2)},mousedown:e.ripple}},[e._v(e._s(e.lang.btn.back))]),e._v(" "),a("a",{on:{click:e.off,mousedown:e.ripple}},[e._v(e._s(e.lang.btn.close))])])]):e._e(),e._v(" "),a("canvas",{directives:[{name:"show",rawName:"v-show",value:!1,expression:"false"}],ref:"canvas",attrs:{width:e.width,height:e.height}})])])},r=[],n=(a("456d"),a("ac6a"),a("7618")),c=(a("c5f6"),a("b775")),l={zh:{hint:"点击，或拖动图片至此处",loading:"正在上传……",noSupported:"浏览器不支持该功能，请使用IE10以上或其他现在浏览器！",success:"上传成功",fail:"图片上传失败",preview:"头像预览",btn:{off:"取消",close:"关闭",back:"上一步",save:"保存"},error:{onlyImg:"仅限图片格式",outOfSize:"单文件大小不能超过 ",lowestPx:"图片最低像素为（宽*高）："}},"zh-tw":{hint:"點擊，或拖動圖片至此處",loading:"正在上傳……",noSupported:"瀏覽器不支持該功能，請使用IE10以上或其他現代瀏覽器！",success:"上傳成功",fail:"圖片上傳失敗",preview:"頭像預覽",btn:{off:"取消",close:"關閉",back:"上一步",save:"保存"},error:{onlyImg:"僅限圖片格式",outOfSize:"單文件大小不能超過 ",lowestPx:"圖片最低像素為（寬*高）："}},en:{hint:"Click or drag the file here to upload",loading:"Uploading…",noSupported:"Browser is not supported, please use IE10+ or other browsers",success:"Upload success",fail:"Upload failed",preview:"Preview",btn:{off:"Cancel",close:"Close",back:"Back",save:"Save"},error:{onlyImg:"Image only",outOfSize:"Image exceeds size limit: ",lowestPx:"Image's size is too low. Expected at least: "}},ro:{hint:"Atinge sau trage fișierul aici",loading:"Se încarcă",noSupported:"Browser-ul tău nu suportă acest feature. Te rugăm încearcă cu alt browser.",success:"S-a încărcat cu succes",fail:"A apărut o problemă la încărcare",preview:"Previzualizează",btn:{off:"Anulează",close:"Închide",back:"Înapoi",save:"Salvează"},error:{onlyImg:"Doar imagini",outOfSize:"Imaginea depășește limita de: ",loewstPx:"Imaginea este prea mică; Minim: "}},ru:{hint:"Нажмите, или перетащите файл в это окно",loading:"Загружаю……",noSupported:"Ваш браузер не поддерживается, пожалуйста, используйте IE10 + или другие браузеры",success:"Загрузка выполнена успешно",fail:"Ошибка загрузки",preview:"Предпросмотр",btn:{off:"Отменить",close:"Закрыть",back:"Назад",save:"Сохранить"},error:{onlyImg:"Только изображения",outOfSize:"Изображение превышает предельный размер: ",lowestPx:"Минимальный размер изображения: "}},"pt-br":{hint:"Clique ou arraste o arquivo aqui para carregar",loading:"Carregando…",noSupported:"Browser não suportado, use o IE10+ ou outro browser",success:"Sucesso ao carregar imagem",fail:"Falha ao carregar imagem",preview:"Pré-visualizar",btn:{off:"Cancelar",close:"Fechar",back:"Voltar",save:"Salvar"},error:{onlyImg:"Apenas imagens",outOfSize:"A imagem excede o limite de tamanho: ",lowestPx:"O tamanho da imagem é muito pequeno. Tamanho mínimo: "}},fr:{hint:"Cliquez ou glissez le fichier ici.",loading:"Téléchargement…",noSupported:"Votre navigateur n'est pas supporté. Utilisez IE10 + ou un autre navigateur s'il vous plaît.",success:"Téléchargement réussit",fail:"Téléchargement echoué",preview:"Aperçu",btn:{off:"Annuler",close:"Fermer",back:"Retour",save:"Enregistrer"},error:{onlyImg:"Image uniquement",outOfSize:"L'image sélectionnée dépasse la taille maximum: ",lowestPx:"L'image sélectionnée est trop petite. Dimensions attendues: "}},nl:{hint:"Klik hier of sleep een afbeelding in dit vlak",loading:"Uploaden…",noSupported:"Je browser wordt helaas niet ondersteund. Gebruik IE10+ of een andere browser.",success:"Upload succesvol",fail:"Upload mislukt",preview:"Voorbeeld",btn:{off:"Annuleren",close:"Sluiten",back:"Terug",save:"Opslaan"},error:{onlyImg:"Alleen afbeeldingen",outOfSize:"De afbeelding is groter dan: ",lowestPx:"De afbeelding is te klein! Minimale afmetingen: "}},tr:{hint:"Tıkla veya yüklemek istediğini buraya sürükle",loading:"Yükleniyor…",noSupported:"Tarayıcı desteklenmiyor, lütfen IE10+ veya farklı tarayıcı kullanın",success:"Yükleme başarılı",fail:"Yüklemede hata oluştu",preview:"Önizle",btn:{off:"İptal",close:"Kapat",back:"Geri",save:"Kaydet"},error:{onlyImg:"Sadece resim",outOfSize:"Resim yükleme limitini aşıyor: ",lowestPx:"Resmin boyutu çok küçük. En az olması gereken: "}},"es-MX":{hint:"Selecciona o arrastra una imagen",loading:"Subiendo...",noSupported:"Tu navegador no es soportado, porfavor usa IE10+ u otros navegadores mas recientes",success:"Subido exitosamente",fail:"Sucedió un error",preview:"Vista previa",btn:{off:"Cancelar",close:"Cerrar",back:"Atras",save:"Guardar"},error:{onlyImg:"Unicamente imagenes",outOfSize:"La imagen excede el tamaño maximo:",lowestPx:"La imagen es demasiado pequeño. Se espera por lo menos:"}},de:{hint:"Klick hier oder zieh eine Datei hier rein zum Hochladen",loading:"Hochladen…",noSupported:"Browser wird nicht unterstützt, bitte verwende IE10+ oder andere Browser",success:"Upload erfolgreich",fail:"Upload fehlgeschlagen",preview:"Vorschau",btn:{off:"Abbrechen",close:"Schließen",back:"Zurück",save:"Speichern"},error:{onlyImg:"Nur Bilder",outOfSize:"Das Bild ist zu groß: ",lowestPx:"Das Bild ist zu klein. Mindestens: "}},ja:{hint:"クリック・ドラッグしてファイルをアップロード",loading:"アップロード中...",noSupported:"このブラウザは対応されていません。IE10+かその他の主要ブラウザをお使いください。",success:"アップロード成功",fail:"アップロード失敗",preview:"プレビュー",btn:{off:"キャンセル",close:"閉じる",back:"戻る",save:"保存"},error:{onlyImg:"画像のみ",outOfSize:"画像サイズが上限を超えています。上限: ",lowestPx:"画像が小さすぎます。最小サイズ: "}}},u={jpg:"image/jpeg",png:"image/png",gif:"image/gif",svg:"image/svg+xml",psd:"image/photoshop"},h=(a("34ef"),a("28a5"),function(e,t){e=e.split(",")[1],e=window.atob(e);for(var a=new Uint8Array(e.length),i=0;i<e.length;i++)a[i]=e.charCodeAt(i);return new Blob([a],{type:t})}),p=function(e,t){var a=Object.assign({ele:e.target,type:"hit",bgc:"rgba(0, 0, 0, 0.15)"},t),i=a.ele;if(i){var s=i.getBoundingClientRect(),o=i.querySelector(".e-ripple");switch(o?o.className="e-ripple":(o=document.createElement("span"),o.className="e-ripple",o.style.height=o.style.width=Math.max(s.width,s.height)+"px",i.appendChild(o)),a.type){case"center":o.style.top=s.height/2-o.offsetHeight/2+"px",o.style.left=s.width/2-o.offsetWidth/2+"px";break;default:o.style.top=e.pageY-s.top-o.offsetHeight/2-document.body.scrollTop+"px",o.style.left=e.pageX-s.left-o.offsetWidth/2-document.body.scrollLeft+"px"}return o.style.backgroundColor=a.bgc,o.className="e-ripple z-active",!1}},g={props:{field:{type:String,default:"avatar"},ki:{default:0},value:{default:!0},url:{type:String,default:""},params:{type:Object,default:null},headers:{type:Object,default:null},width:{type:Number,default:200},height:{type:Number,default:200},noRotate:{type:Boolean,default:!0},noCircle:{type:Boolean,default:!1},noSquare:{type:Boolean,default:!1},maxSize:{type:Number,default:10240},langType:{type:String,default:"zh"},langExt:{type:Object,default:null},imgFormat:{type:String,default:"png"},withCredentials:{type:Boolean,default:!1}},data:function(){var e=this,t=e.imgFormat,a=e.langType,i=e.langExt,s=e.width,o=e.height,r=!0,n=["jpg","png"],c=-1===n.indexOf(t)?"jpg":t,h=l[a]?l[a]:l["en"],p=u[c];return e.imgFormat=c,i&&Object.assign(h,i),"function"!==typeof FormData&&(r=!1),{mime:p,lang:h,isSupported:r,isSupportTouch:document.hasOwnProperty("ontouchstart"),step:1,loading:0,progress:0,hasError:!1,errorMsg:"",ratio:s/o,sourceImg:null,sourceImgUrl:"",createImgUrl:"",sourceImgMouseDown:{on:!1,mX:0,mY:0,x:0,y:0},previewContainer:{width:100,height:100},sourceImgContainer:{width:240,height:184},scale:{zoomAddOn:!1,zoomSubOn:!1,range:1,rotateLeft:!1,rotateRight:!1,degree:0,x:0,y:0,width:0,height:0,maxWidth:0,maxHeight:0,minWidth:0,minHeight:0,naturalWidth:0,naturalHeight:0}}},computed:{progressStyle:function(){var e=this.progress;return{width:e+"%"}},sourceImgStyle:function(){var e=this.scale,t=this.sourceImgMasking,a=e.y+t.y+"px",i=e.x+t.x+"px";return{top:a,left:i,width:e.width+"px",height:e.height+"px",transform:"rotate("+e.degree+"deg)","-ms-transform":"rotate("+e.degree+"deg)","-moz-transform":"rotate("+e.degree+"deg)","-webkit-transform":"rotate("+e.degree+"deg)","-o-transform":"rotate("+e.degree+"deg)"}},sourceImgMasking:function(){var e=this.width,t=this.height,a=this.ratio,i=this.sourceImgContainer,s=i,o=s.width/s.height,r=0,n=0,c=s.width,l=s.height,u=1;return a<o&&(u=s.height/t,c=s.height*a,r=(s.width-c)/2),a>o&&(u=s.width/e,l=s.width/a,n=(s.height-l)/2),{scale:u,x:r,y:n,width:c,height:l}},sourceImgShadeStyle:function(){var e=this.sourceImgMasking,t=this.sourceImgContainer,a=t,i=e,s=i.width==a.width?i.width:(a.width-i.width)/2,o=i.height==a.height?i.height:(a.height-i.height)/2;return{width:s+"px",height:o+"px"}},previewStyle:function(){this.width,this.height;var e=this.ratio,t=this.previewContainer,a=t,i=a.width,s=a.height,o=i/s;return e<o&&(i=a.height*e),e>o&&(s=a.width/e),{width:i+"px",height:s+"px"}}},watch:{value:function(e){e&&1!=this.loading&&this.reset()}},methods:{ripple:function(e){p(e)},off:function(){var e=this;setTimeout(function(){e.$emit("input",!1),e.$emit("close"),3==e.step&&2==e.loading&&e.setStep(1)},200)},setStep:function(e){var t=this;setTimeout(function(){t.step=e},200)},preventDefault:function(e){return e.preventDefault(),!1},handleClick:function(e){1!==this.loading&&e.target!==this.$refs.fileinput&&(e.preventDefault(),document.activeElement!==this.$refs&&this.$refs.fileinput.click())},handleChange:function(e){if(e.preventDefault(),1!==this.loading){var t=e.target.files||e.dataTransfer.files;this.reset(),this.checkFile(t[0])&&this.setSourceImg(t[0])}},checkFile:function(e){var t=this,a=t.lang,i=t.maxSize;return-1===e.type.indexOf("image")?(t.hasError=!0,t.errorMsg=a.error.onlyImg,!1):!(e.size/1024>i)||(t.hasError=!0,t.errorMsg=a.error.outOfSize+i+"kb",!1)},reset:function(){var e=this;e.loading=0,e.hasError=!1,e.errorMsg="",e.progress=0},setSourceImg:function(e){var t=this,a=new FileReader;a.onload=function(e){t.sourceImgUrl=a.result,t.startCrop()},a.readAsDataURL(e)},startCrop:function(){var e=this,t=e.width,a=e.height,i=e.ratio,s=e.scale,o=e.sourceImgUrl,r=e.sourceImgMasking,n=e.lang,c=r,l=new Image;l.src=o,l.onload=function(){var o=l.naturalWidth,r=l.naturalHeight,u=o/r,h=c.width,p=c.height,g=0,d=0;if(o<t||r<a)return e.hasError=!0,e.errorMsg=n.error.lowestPx+t+"*"+a,!1;i>u&&(p=h/u,d=(c.height-p)/2),i<u&&(h=p*u,g=(c.width-h)/2),s.range=0,s.x=g,s.y=d,s.width=h,s.height=p,s.degree=0,s.minWidth=h,s.minHeight=p,s.maxWidth=o*c.scale,s.maxHeight=r*c.scale,s.naturalWidth=o,s.naturalHeight=r,e.sourceImg=l,e.createImg(),e.setStep(2)}},imgStartMove:function(e){if(e.preventDefault(),this.isSupportTouch&&!e.targetTouches)return!1;var t=e.targetTouches?e.targetTouches[0]:e,a=this.sourceImgMouseDown,i=this.scale,s=a;s.mX=t.screenX,s.mY=t.screenY,s.x=i.x,s.y=i.y,s.on=!0},imgMove:function(e){if(e.preventDefault(),this.isSupportTouch&&!e.targetTouches)return!1;var t=e.targetTouches?e.targetTouches[0]:e,a=this.sourceImgMouseDown,i=a.on,s=a.mX,o=a.mY,r=a.x,n=a.y,c=this.scale,l=this.sourceImgMasking,u=l,h=t.screenX,p=t.screenY,g=h-s,d=p-o,m=r+g,v=n+d;i&&(m>0&&(m=0),v>0&&(v=0),m<u.width-c.width&&(m=u.width-c.width),v<u.height-c.height&&(v=u.height-c.height),c.x=m,c.y=v)},startRotateRight:function(e){var t=this,a=t.scale;function i(){if(a.rotateRight){var e=++a.degree;t.createImg(e),setTimeout(function(){i()},60)}}a.rotateRight=!0,i()},startRotateLeft:function(e){var t=this,a=t.scale;function i(){if(a.rotateLeft){var e=--a.degree;t.createImg(e),setTimeout(function(){i()},60)}}a.rotateLeft=!0,i()},endRotate:function(){var e=this.scale;e.rotateLeft=!1,e.rotateRight=!1},startZoomAdd:function(e){var t=this,a=t.scale;function i(){if(a.zoomAddOn){var e=a.range>=100?100:++a.range;t.zoomImg(e),setTimeout(function(){i()},60)}}a.zoomAddOn=!0,i()},endZoomAdd:function(e){this.scale.zoomAddOn=!1},startZoomSub:function(e){var t=this,a=t.scale;function i(){if(a.zoomSubOn){var e=a.range<=0?0:--a.range;t.zoomImg(e),setTimeout(function(){i()},60)}}a.zoomSubOn=!0,i()},endZoomSub:function(e){var t=this.scale;t.zoomSubOn=!1},zoomChange:function(e){this.zoomImg(e.target.value)},zoomImg:function(e){var t=this,a=this.sourceImgMasking,i=(this.sourceImgMouseDown,this.scale),s=i.maxWidth,o=i.maxHeight,r=i.minWidth,n=i.minHeight,c=i.width,l=i.height,u=i.x,h=i.y,p=(i.range,a),g=p.width,d=p.height,m=r+(s-r)*e/100,v=n+(o-n)*e/100,f=g/2-m/c*(g/2-u),w=d/2-v/l*(d/2-h);f>0&&(f=0),w>0&&(w=0),f<g-m&&(f=g-m),w<d-v&&(w=d-v),i.x=f,i.y=w,i.width=m,i.height=v,i.range=e,setTimeout(function(){i.range==e&&t.createImg()},300)},createImg:function(e){var t=this,a=t.mime,i=t.sourceImg,s=t.scale,o=s.x,r=s.y,n=s.width,c=s.height,l=s.degree,u=t.sourceImgMasking.scale,h=t.$refs.canvas,p=h.getContext("2d");e&&(t.sourceImgMouseDown.on=!1),h.width=t.width,h.height=t.height,p.clearRect(0,0,t.width,t.height),p.fillStyle="#fff",p.fillRect(0,0,t.width,t.height),p.translate(.5*t.width,.5*t.height),p.rotate(Math.PI*l/180),p.translate(.5*-t.width,.5*-t.height),p.drawImage(i,o/u,r/u,n/u,c/u),t.createImgUrl=h.toDataURL(a)},prepareUpload:function(){var e=this.url,t=this.createImgUrl,a=this.field,i=this.ki;this.$emit("crop-success",t,a,i),"string"===typeof e&&e?this.upload():this.off()},upload:function(){var e=this,t=this.lang,a=this.imgFormat,i=this.mime,s=this.url,o=this.params,r=(this.headers,this.field),l=this.ki,u=this.createImgUrl,p=(this.withCredentials,new FormData);p.append(r,h(u,i),r+"."+a),"object"===Object(n["a"])(o)&&o&&Object.keys(o).forEach(function(e){p.append(e,o[e])});e.reset(),e.loading=1,e.setStep(3),Object(c["a"])({url:s,method:"post",data:p}).then(function(t){e.loading=2,e.$emit("crop-upload-success",t.data)}).catch(function(a){e.value&&(e.loading=3,e.hasError=!0,e.errorMsg=t.fail,e.$emit("crop-upload-fail",a,r,l))})}},created:function(){var e=this;document.addEventListener("keyup",function(t){!e.value||"Escape"!=t.key&&27!=t.keyCode||e.off()})}},d=g,m=(a("2201"),a("2877")),v=Object(m["a"])(d,o,r,!1,null,null,null),f=v.exports,w=a("3cbc"),y={name:"AvatarUploadDemo",components:{ImageCropper:f,PanThumb:w["a"]},data:function(){return{imagecropperShow:!1,imagecropperKey:0,image:"https://wpimg.wallstcn.com/577965b9-bb9e-4e02-9f0c-095b41417191"}},methods:{cropSuccess:function(e){this.imagecropperShow=!1,this.imagecropperKey=this.imagecropperKey+1,this.image=e.files.avatar},close:function(){this.imagecropperShow=!1}}},b=y,S=(a("636d"),Object(m["a"])(b,i,s,!1,null,"35593a91",null));t["default"]=S.exports},ad09:function(e,t,a){},d23e:function(e,t,a){}}]);