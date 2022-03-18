import {Igrac} from "./Igrac.js"
import {Utakmica} from "./Utakmica.js"


var queryString = decodeURIComponent(window.location.search);
var sezona = queryString.substring(6,10);
var klub = queryString.substring(16);

var kontejner = document.createElement("div");
document.body.appendChild(kontejner);
kontejner.className = "glavnidiv";

var pomocnikontejner = document.createElement("div");


CrtajHeader(klub,kontejner);
CrtajIgrace(klub,sezona,pomocnikontejner);
cratjGraf(klub,sezona,pomocnikontejner);
kontejner.appendChild(pomocnikontejner);
crtajUtakmice(klub,sezona,kontejner);



function CrtajHeader(klub,glavniKontejner)
{
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

    let kontejner = document.createElement("div");
    header.appendChild(kontejner);

    let labela = document.createElement("label");
    labela.innerHTML=klub;
    kontejner.appendChild(labela);

    let grb = document.createElement("img");
    grb.src = "grbovi/" + klub + ".jpg"
    grb.height = "50";
    grb.width = "50";
    grb.style.position = "relative";
    grb.style.top="7px";
    grb.style.marginLeft = "15px";
    kontejner.appendChild(grb);
    kontejner.style.paddingRight = "10px";
}

function CrtajIgrace(klub,sezona,glavniKontejner)
{
    var kontejner = document.createElement("div");
    glavniKontejner.appendChild(kontejner);
    kontejner.className = "hdiv";
    kontejner.style.alignItems = "flex-start";

    var kontejnerTabela = document.createElement("div");
    kontejner.appendChild(kontejnerTabela);

    var table = document.createElement("table");
    kontejnerTabela.appendChild(table);

    var tr = document.createElement("tr");
    let th;
    var Head = ["Ime", "Prezime", "Godina rodjenja", "Drzavljanstvo", "Golovi", "Asistencije"];
    Head.forEach(el => {
        th = document.createElement("th");
        th.innerHTML = el;
        tr.appendChild(th);
    })
    table.appendChild(tr);

    var listaIgraca=[];
    fetch("https://localhost:5001/Igrac/Svi_igraci_klub/" + klub + "/"+ sezona)
    .then(p=>{
        p.json().then(igraci=>{
            igraci.forEach(igrac=>{
                var i = new Igrac(igrac.ime, igrac.prezime, igrac.godinaRodjenja, igrac.nacionalnost, igrac.golovi, igrac.asistencije, igrac.klub);
                listaIgraca.push(i);

                var tr = document.createElement("tr");
                table.appendChild(tr);

                var el = document.createElement("td");
                el.innerHTML = i.ime;
                tr.appendChild(el);

                var el = document.createElement("td");
                el.innerHTML = i.prezime;
                tr.appendChild(el);

                var el = document.createElement("td");
                el.innerHTML = i.godinarodjenja;
                tr.appendChild(el);

                var el = document.createElement("td");
                el.innerHTML = i.nacionalnost;
                tr.appendChild(el);

                var el = document.createElement("td");
                el.innerHTML = i.golovi;
                tr.appendChild(el);

                el.style.cursor = "pointer"

                el.addEventListener("click", (e) => {
                    var ime = tr.cells[0].innerHTML;
                    var prezime = tr.cells[1].innerHTML;
                    let golovi=prompt("Unesite broj golova","");
                    golovi = parseInt(golovi);
                    fetch("https://localhost:5001/Igrac/Promeni_golove/" + ime + "/" + prezime + "/" + golovi + "/" + klub + "/" + sezona,{
                        method: 'PUT'})
                    document.location.reload(true);
                })

                var el = document.createElement("td");
                el.innerHTML = i.asistencije;
                tr.appendChild(el);

                el.style.cursor = "pointer"

                el.addEventListener("click", (e) => {
                    var ime = tr.cells[0].innerHTML;
                    var prezime = tr.cells[1].innerHTML;
                    let asistencije=prompt("Unesite broj asistencija","");
                    asistencije = parseInt(asistencije);
                    fetch("https://localhost:5001/Igrac/Promeni_asistencije/" + ime + "/" + prezime + "/" + asistencije + "/" + klub + "/" + sezona,{
                        method: 'PUT'})
                    document.location.reload(true);
                })

                var el = document.createElement("td");
                el.innerHTML = "Obrisi";
                tr.appendChild(el);

                el.style.cursor = "pointer"

                el.addEventListener("click", (e) => {
                    var ime = tr.cells[0].innerHTML;
                    var prezime = tr.cells[1].innerHTML;
                    fetch("https://localhost:5001/Igrac/Brisanje_igraca/" + ime + "/" + prezime + "/" + klub + "/" + sezona,{
                    method: 'DELETE'})
                    document.location.reload(true);
                })
            })
        })
    })

    var kontejnerForma = document.createElement("div");
    kontejner.appendChild(kontejnerForma);

    var lista1 = ["Ime:", "Prezime:", "Godina rodjenja:", "Drzavljanstvo:", "Golovi:", "Asistencije:"];
    var lista2 = ["ime", "prezime", "godina_rodjenja", "drzavljanstvo", "golovi", "asistencije"];

    for (let i=0;i<lista1.length;i++)
    {
        let elLabela = document.createElement("label");
        elLabela.innerHTML=lista1[i];
        kontejnerForma.appendChild(elLabela);

        let tb= document.createElement("input");
        tb.className=lista2[i];
        kontejnerForma.appendChild(tb);
    }

    const dugme = document.createElement("button");
    dugme.innerHTML="Dodaj igraca";
    kontejnerForma.appendChild(dugme);
    dugme.onclick=(ev)=>{
        fetch("https://localhost:5001/Igrac/Unos_igraca/" + kontejner.querySelector(".ime").value + "/" + kontejner.querySelector(".prezime").value + "/" + kontejner.querySelector(".godina_rodjenja").value + "/" + kontejner.querySelector(".drzavljanstvo").value  + "/" + kontejner.querySelector(".golovi").value + "/" + kontejner.querySelector(".asistencije").value + "/" + klub + "/" + sezona,{
            method: 'POST'})
        document.location.reload(true);
    }

    kontejnerForma.style.paddingLeft = "20px"
}

async function crtajUtakmice(klub,sezona,glavniKontejner){
    var kontejner = document.createElement("div");
    glavniKontejner.appendChild(kontejner);
    kontejner.style.padding = "5px";

    var table = document.createElement("table");
    kontejner.appendChild(table);


    var tr = document.createElement("tr");
    let th;
    var Head = ["Kolo", "Domacin", "", "", "Gost", "Sudija"];
    Head.forEach(el => {
        th = document.createElement("th");
        th.innerHTML = el;
        tr.appendChild(th);
    })
    table.appendChild(tr);

    var listaUtakmica=[];
    let result = await fetch("https://localhost:5001/Sezona/Sve_utakmice_klub/" + sezona + "/"+ klub)
    let utakmice = await result.json();
    utakmice.forEach(async (utakmica) =>{
        var u = new Utakmica(utakmica.domacin, utakmica.golovi_domacin, utakmica.gost, utakmica.golovi_gost, utakmica.sezona, utakmica.kolo,utakmica.sudija);
        listaUtakmica.push(u);
        u.crtajUtakmicu(table);
    })

    var rows,switching, i, x, y, shouldSwitch;
    rows=table.rows;
    switching = true;

    while (switching) {
        switching = false;
        for (i = 1; i < rows.length-1; i++) {
            shouldSwitch = false;

            x = rows[i].childNodes[0];
            y = rows[i+1].childNodes[0];

            if (parseInt(x.innerHTML) > parseInt(y.innerHTML)) {
                shouldSwitch = true;
                break;
            }
        }
        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
        }
    }

}

async function cratjGraf(klub,sezona,glavniKontejner){
    var kontejner = document.createElement("div");
    glavniKontejner.appendChild(kontejner);

    var bodovi = 0;
    var listaUtakmica=[];
    let result = await fetch("https://localhost:5001/Sezona/Sve_utakmice_klub/" + sezona + "/"+ klub);
    let utakmice = await result.json();
    utakmice.forEach(async (utakmica) =>{
        var u = new Utakmica(utakmica.domacin, utakmica.golovi_domacin, utakmica.gost, utakmica.golovi_gost, utakmica.sezona, utakmica.kolo,utakmica.sudija);
        listaUtakmica.push(u);
    })

    for(var i=0; i<listaUtakmica.length; i++)
    {
        for(var j=0; j<listaUtakmica.length-1; j++)
        {
            if (listaUtakmica[j].kolo > listaUtakmica[j+1].kolo)
            {
                var pom=listaUtakmica[j];
                listaUtakmica[j]=listaUtakmica[j+1];
                listaUtakmica[j+1]=pom;
            }
        }
    }

    var yValues = [0],xValues = [];
    listaUtakmica.forEach(k=>{
        if (k.domacin.naziv == klub){
                if (k.golovi_domacin > k.golovi_gost)
                {
                    bodovi+=3;
                }
                if (k.golovi_domacin == k.golovi_gost)
                {
                    bodovi+=1;
                }
        }
        if (k.gost.naziv == klub){
                if (k.golovi_gost > k.golovi_domacin)
                {
                    bodovi+=3;
                }
                if (k.golovi_gost == k.golovi_domacin)
                {
                    bodovi+=1;
                }
        }
        yValues.push(bodovi);
    })


    for(var i=0;i<39;i++){
        xValues.push(i);
    }

    new Chart("myChart", {
        type: "line",
        data: {
            labels: xValues,
            datasets: [{
                fill: false,
                lineTension: 0,
                backgroundColor: "rgb(56, 4, 60)",
                borderColor: "rgb(56, 4, 60)",
                data: yValues
            }]
        },
        options: {
            legend: {display: false},
            scales: {
                yAxes: [{ticks: {min: 0, max:100}}],
            }
        }
    });
    
    kontejner.appendChild(myChart);
}