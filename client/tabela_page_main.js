import {Utakmica} from "./Utakmica.js"
import {Igrac} from "./Igrac.js"

var kontejner = document.createElement("div");
document.body.appendChild(kontejner);
kontejner.className = "glavnidiv";

crtajHeader(kontejner);

function crtajHeader(glavniKontejner){
    var header = document.createElement("header");
    glavniKontejner.appendChild(header);

    let grb_pl = document.createElement("img");
    grb_pl.src ="PL.jpg"
    grb_pl.height = "60";
    grb_pl.width = "140";
    header.appendChild(grb_pl);
    grb_pl.style.cursor = "pointer";

    grb_pl.addEventListener("click",(e) => {
        window.location = "http://127.0.0.1:5500/client/tabela_page.html";
    })

    let labela = document.createElement("label");
    labela.innerHTML="Premier League";
    header.appendChild(labela);

    let kontejner = document.createElement("div");
    header.appendChild(kontejner);

    let sel= document.createElement("select");
    labela = document.createElement("label");
    labela.innerHTML="Sezona:"
    header.appendChild(labela);
    header.appendChild(sel);
    labela.style.fontSize="20px";
    labela.style.fontWeight = "normal";
    let opcija0 = document.createElement("option");
    opcija0.innerHTML = "Izaberite sezonu";
    sel.appendChild(opcija0);
    fetch("https://localhost:5001/Sezona/Pregledaj_sezone")
    .then(p=>{
        p.json().then(sezone=>{
            sezone.forEach(sezona=>{
                let opcija=document.createElement("option");
                opcija.innerHTML=sezona.godina;
                opcija.value=sezona.godina;
                sel.appendChild(opcija);
            })
        })
    })
    kontejner.appendChild(labela);
    kontejner.appendChild(sel);
    sel.style.height = "20px";
    kontejner.style.display = "flex";
    kontejner.style.flexDirection = "row";
    kontejner.style.alignItems = "center";


    sel.onchange = function() {
        crtaj(sel.value,glavniKontejner);
    }
}

async function crtaj(sezona,kontejner) {
    kontejner.innerHTML = "";

    crtajHeader(kontejner);
    crtajTabelu(sezona,kontejner);
    crtajKolo(sezona,kontejner);
    crtajStrelce(sezona,kontejner);
    crtajAsistente(sezona,kontejner);
}

async function crtajTabelu(sezona,glavniKontejner){
    var kontejner = document.createElement("div");
    glavniKontejner.appendChild(kontejner);

    kontejner.className = "hdiv";

    var table = document.createElement("table");
    kontejner.appendChild(table);

    var tr = document.createElement("tr");
    let th;
    var Head = ["Klub","Pobede","Remiji","Porazi","Dati golovi","Primljeni golovi","Gol razlika","Bodovi"];
    Head.forEach(el => {
        th = document.createElement("th");
        th.innerHTML = el;
        tr.appendChild(th);
    })
    table.appendChild(tr);


    let result = await fetch("https://localhost:5001/Klub/Svi_klubovi/" + sezona);
    let klubovi = await result.json();
    for(let i = 0; i < klubovi.length; i++){
        let resultUtakmice = await fetch("https://localhost:5001/Sezona/Sve_utakmice_klub/" + sezona +"/" + klubovi[i].naziv);
        let utakmiceKlub = await resultUtakmice.json();
        var pobede=0,remiji=0,porazi=0,golovi_d=0,golovi_p=0;
        utakmiceKlub.forEach(async (utakmica) => {
            var k = new Utakmica(utakmica.domacin, utakmica.golovi_domacin, utakmica.gost, utakmica.golovi_gost, utakmica.sezona, utakmica.kolo,utakmica.sudija);
            var naziv = klubovi[i].naziv;
            if (k.domacin.naziv == naziv)
            {
                golovi_d+=k.golovi_domacin;
                golovi_p+=k.golovi_gost;
                if (k.golovi_domacin > k.golovi_gost)
                {
                    pobede++;
                }
                if (k.golovi_domacin == k.golovi_gost)
                {
                    remiji++;
                }
                if (k.golovi_domacin < k.golovi_gost)
                {
                    porazi++;
                }
            }
            if (k.gost.naziv == naziv)
            {
                golovi_d+=k.golovi_gost;
                golovi_p+=k.golovi_domacin;
                if (k.golovi_gost > k.golovi_domacin)
                {
                    pobede++;
                }
                if (k.golovi_gost == k.golovi_domacin)
                {
                   remiji++;
                }
                if (k.golovi_gost < k.golovi_domacin)
                {
                    porazi++;
                }
            }
        })
        var bodovi = pobede*3 + remiji;
        var gol_razlika = golovi_d - golovi_p;

        var tr = document.createElement("tr");
        table.appendChild(tr);

        var el = document.createElement("td");
        el.innerHTML = klubovi[i].naziv;
        tr.appendChild(el);

        var el = document.createElement("td");
        el.innerHTML = pobede;
        tr.appendChild(el);

        var el = document.createElement("td");
        el.innerHTML = remiji;
        tr.appendChild(el);

        var el = document.createElement("td");
        el.innerHTML = porazi;
        tr.appendChild(el);

        var el = document.createElement("td");
        el.innerHTML = golovi_d;
        tr.appendChild(el);
                        
        var el = document.createElement("td");
        el.innerHTML = golovi_p;
        tr.appendChild(el);

        var el = document.createElement("td");
        el.innerHTML = gol_razlika;
        tr.appendChild(el);
        
        var el = document.createElement("td");
        el.innerHTML = bodovi;
        tr.appendChild(el);                        
    }
        
    var rows,switching, i, t1b, t1gr, t2b, t2gr, shouldSwitch;
    rows=table.rows;
    switching = true;

    while (switching) {
        switching = false;

        for (i = 0; i < rows.length-1; i++) {
            shouldSwitch = false;
            t1b = rows[i].childNodes[7];
            t2b = rows[i+1].childNodes[7];
            t1gr = rows[i].childNodes[6];
            t2gr = rows[i+1].childNodes[6];

            if (parseInt(t1b.innerHTML) < parseInt(t2b.innerHTML)) {
                shouldSwitch = true;
                break;
            }
            if (parseInt(t1b.innerHTML) == parseInt(t2b.innerHTML) && parseInt(t1gr.innerHTML) < parseInt(t2gr.innerHTML)){
                shouldSwitch = true;
                break;
            }
        }
        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
        }
    }

    for (var i = 1,row; row = table.rows[i]; i++) {
        var col = row.cells[0];
        col.style.cursor = "pointer"
        col.addEventListener("click", (e) => {
            var id = e.target.innerHTML;
            var value1= id;
            var queryString = "?para1" + sezona + "&para2" + value1;
            window.location = "http://127.0.0.1:5500/client/klub_page.html" + queryString;
        })
    }

    let trofej = document.createElement("img");
    trofej.src ="trofej.jpg"
    trofej.height = "408";
    trofej.width = "212";
    kontejner.appendChild(trofej);
    
    trofej.style.paddingLeft = "50px";
}

async function crtajStrelce(sezona,glavniKontejner){
    var igraciLista = [];
    var result = await fetch("https://localhost:5001/Igrac/Svi_igraci/" + sezona)
    let igraci = await result.json();
    igraci.forEach(async (i)=>
    {
        var igrac = new Igrac (i.ime, i.prezime, i.godinarodjenja, i.nacionalnost, i.golovi, i.asistencije, i.klub)
        igraciLista.push(igrac); 
    })

    for(var i=0; i<igraciLista.length; i++)
    {
        for(var j=0; j<igraciLista.length-1; j++)
        {
            if (igraciLista[j].golovi< igraciLista[j+1].golovi)
            {
                var pom=igraciLista[j];
                igraciLista[j]=igraciLista[j+1];
                igraciLista[j+1]=pom;
            }
        }
    }

    var kontejner = document.createElement("div");
    glavniKontejner.appendChild(kontejner);
    kontejner.className = "hdiv";


    var table = document.createElement("table");
    kontejner.appendChild(table);

    var tr = document.createElement("tr");
    var Head = ["Ime", "Prezime","Klub","Golovi"];
    Head.forEach(el => {
        let th = document.createElement("th");
        th.innerHTML = el;
        tr.appendChild(th);
    })
    table.appendChild(tr);


    for(var i=0; i<15; i++)
    {
        var tr = document.createElement("tr");
        table.appendChild(tr);
        
        var el = document.createElement("td");
        el.innerHTML = igraciLista[i].ime;
        tr.appendChild(el);
        
        var el = document.createElement("td");
        el.innerHTML = igraciLista[i].prezime;
        tr.appendChild(el);
        
        var el = document.createElement("td");
        el.innerHTML = igraciLista[i].klub.naziv;
        tr.appendChild(el);  

        var el = document.createElement("td");
        el.innerHTML =igraciLista[i].golovi;
        tr.appendChild(el);     
    }

    let trofej = document.createElement("img");
    trofej.src ="strelac.jpg"
    trofej.height = "200";
    trofej.width = "200";
    kontejner.appendChild(trofej);
    
    trofej.style.paddingLeft = "50px";
}

async function crtajAsistente(sezona,glavniKontejner){
    var igraciLista = [];
    var result = await fetch("https://localhost:5001/Igrac/Svi_igraci/" + sezona)
    let igraci = await result.json();
    igraci.forEach(async (i)=>
    {
        var igrac = new Igrac (i.ime, i.prezime, i.godinarodjenja, i.nacionalnost, i.golovi, i.asistencije, i.klub)
        igraciLista.push(igrac); 
    })

    for(var i=0; i<igraciLista.length; i++)
    {
        for(var j=0; j<igraciLista.length-1; j++)
        {
            if (igraciLista[j].asistencije < igraciLista[j+1].asistencije)
            {
                var pom=igraciLista[j];
                igraciLista[j]=igraciLista[j+1];
                igraciLista[j+1]=pom;
            }
        }
    }

    var kontejner = document.createElement("div");
    glavniKontejner.appendChild(kontejner);
    kontejner.className = "hdiv";


    var table = document.createElement("table");
    kontejner.appendChild(table);

    var tr = document.createElement("tr");
    var Head = ["Ime", "Prezime","Klub","Asistencije"];
    Head.forEach(el => {
        let th = document.createElement("th");
        th.innerHTML = el;
        tr.appendChild(th);
    })
    table.appendChild(tr);

    for(var i=0; i<15; i++)
    {
        var tr = document.createElement("tr");
        table.appendChild(tr);
        
        var el = document.createElement("td");
        el.innerHTML = igraciLista[i].ime;
        tr.appendChild(el);
        
        var el = document.createElement("td");
        el.innerHTML = igraciLista[i].prezime;
        tr.appendChild(el);
        
        var el = document.createElement("td");
        el.innerHTML = igraciLista[i].klub.naziv;
        tr.appendChild(el);  

        var el = document.createElement("td");
        el.innerHTML =igraciLista[i].asistencije;
        tr.appendChild(el);     
    }

    let trofej = document.createElement("img");
    trofej.src ="asistent.jpg"
    trofej.height = "200";
    trofej.width = "200";
    kontejner.appendChild(trofej);
    
    trofej.style.paddingLeft = "50px";
}

async function crtajKolo(sezona,glavniKontejner){
    var listaUtakmica=[];

    var kontejner = document.createElement("div");
    glavniKontejner.appendChild(kontejner);
    kontejner.className = "hdiv";
    kontejner.style.alignItems = "flex-start";

    var kontejnerTabela = document.createElement("div");
    kontejner.appendChild(kontejnerTabela);

    var max = 1;
    let resultUtakmice = await fetch("https://localhost:5001/Sezona/Sve_utakmice/" + sezona);
    let utakmice = await resultUtakmice.json();
    utakmice.forEach(utakmica => {
        if (max < utakmica.kolo)
        {
            max=utakmica.kolo;
        }
    })

    let sel= document.createElement("select");
    let labela = document.createElement("label");
    labela.innerHTML="Kolo:"
    kontejnerTabela.appendChild(labela);
    kontejnerTabela.appendChild(sel);
    sel.className="kolo";
    for(let i=1; i<39;i++){
        let opcija=document.createElement("option");
        opcija.innerHTML=i;
        opcija.value=i;
        sel.appendChild(opcija);
    }



    var table = document.createElement("table");
    kontejnerTabela.appendChild(table);

    var tr2 = document.createElement("tr");
    let th2;
    var Head = ["Kolo", "Domacin", "", "", "Gost", "Sudija"];
    Head.forEach(el => {
        th2 = document.createElement("th");
        th2.innerHTML = el;
        tr2.appendChild(th2);
    })
    table.appendChild(tr2);
    table.style.marginTop = "5px";

    fetch("https://localhost:5001/Sezona/Pogledaj_kolo/" + sezona + "/"+ max)
    .then(p=>{
        p.json().then(utakmice=>{
            utakmice.forEach(utakmica=>{
                var u = new Utakmica(utakmica.domacin, utakmica.golovi_domacin, utakmica.gost, utakmica.golovi_gost, utakmica.sezona, utakmica.kolo,utakmica.sudija);
                listaUtakmica.push(u);
                u.crtajUtakmicu(table);
            })
        })
    })

    sel.onchange = function() {
        if(table.rows.length != 1){
            for (let i=table.rows.length-1;i>=1;i--){
                table.deleteRow(i);
            }
        }

        fetch("https://localhost:5001/Sezona/Pogledaj_kolo/" + sezona + "/"+ kontejner.querySelector(".kolo").value)
        .then(p=>{
            p.json().then(utakmice=>{
                utakmice.forEach(utakmica=>{
                    var u = new Utakmica(utakmica.domacin, utakmica.golovi_domacin, utakmica.gost, utakmica.golovi_gost, utakmica.sezona, utakmica.kolo,utakmica.sudija);
                    listaUtakmica.push(u);
                    u.crtajUtakmicu(table);
                })
            })
        })
    }

    var kontejnerForma = document.createElement("div");
    kontejner.appendChild(kontejnerForma);
    kontejnerForma.style.paddingTop = "40px";
    kontejnerForma.style.paddingLeft = "10px";

    let result = await fetch("https://localhost:5001/Klub/Svi_klubovi/" + sezona);
    let klubovi = await result.json();

    labela = document.createElement("label");
    labela.innerHTML="Domacin:";
    kontejnerForma.appendChild(labela);
    sel= document.createElement("select");
    sel.className="domacin";
    kontejnerForma.appendChild(sel);
    let opcija0 = document.createElement("option");
    opcija0.innerHTML = "Izaberite klub";
    sel.appendChild(opcija0);
    klubovi.forEach(k => {
        let opcija=document.createElement("option");
        opcija.innerHTML=k.naziv;
        sel.appendChild(opcija);
    })

    labela = document.createElement("label");
    labela.innerHTML="Golovi domacin:";
    kontejnerForma.appendChild(labela);
    let input = document.createElement("input");
    input.className="golovi_domacin";
    kontejnerForma.appendChild(input);

    labela = document.createElement("label");
    labela.innerHTML="Gost:";
    kontejnerForma.appendChild(labela);
    sel= document.createElement("select");
    sel.className="gost";
    kontejnerForma.appendChild(sel);
    opcija0 = document.createElement("option");
    opcija0.innerHTML = "Izaberite klub";
    sel.appendChild(opcija0);
    klubovi.forEach(k => {
        let opcija=document.createElement("option");
        opcija.innerHTML=k.naziv;
        sel.appendChild(opcija);
    })

    labela = document.createElement("label");
    labela.innerHTML="Golovi gost:";
    kontejnerForma.appendChild(labela);
    input= document.createElement("input");
    input.className="golovi_gost";
    kontejnerForma.appendChild(input);

    sel= document.createElement("select");
    sel.className="sudija";
    labela = document.createElement("label");
    labela.innerHTML="Sudije:"
    kontejnerForma.appendChild(labela);
    kontejnerForma.appendChild(sel);
    opcija0 = document.createElement("option");
    opcija0.innerHTML = "Izaberite sudiju";
    sel.appendChild(opcija0);
    fetch("https://localhost:5001/Sudija/Sve_sudije")
    .then(p=>{
        p.json().then(sudije=>{
            sudije.forEach(sudija=>{
                let opcija=document.createElement("option");
                opcija.innerHTML=sudija.ime+ " " + sudija.prezime;
                sel.appendChild(opcija);
            })
        })
    })

    const dugme = document.createElement("button");
    dugme.innerHTML="Dodaj utakmicu";
    kontejnerForma.appendChild(dugme);
    dugme.style.marginTop = "5px";

    dugme.onclick=(ev)=>{
        const myArray = kontejner.querySelector(".sudija").value.split(" ");
        fetch("https://localhost:5001/Sezona/Unos_utakmice/" + sezona + "/"+ kontejner.querySelector(".domacin").value + "/" + kontejner.querySelector(".golovi_domacin").value + "/" + kontejner.querySelector(".gost").value + "/" + kontejner.querySelector(".golovi_gost").value + "/" + myArray[0] + "/" + myArray[1]+ "/" + kontejner.querySelector(".kolo").value,{
            method: 'POST'})
        document.location.reload(true);
    }
}
