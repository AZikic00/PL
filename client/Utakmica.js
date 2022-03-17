export class Utakmica {

    constructor(domacin, golovi_domacin, gost, golovi_gost, sezona, kolo,sudija) {
        this.domacin = domacin;
        this.golovi_domacin = golovi_domacin;
        this.gost = gost;
        this.golovi_gost = golovi_gost;
        this.sezona = sezona;
        this.kolo = kolo;
        this.sudija = sudija;
    }

    crtajUtakmicu(table) {

        var tr = document.createElement("tr");
        table.appendChild(tr);

        var el = document.createElement("td");
        el.innerHTML = this.kolo;
        tr.appendChild(el);

        var el = document.createElement("td");
        el.innerHTML = this.domacin.naziv;
        tr.appendChild(el);

        var el = document.createElement("td");
        el.innerHTML = this.golovi_domacin;
        tr.appendChild(el);

        var el = document.createElement("td");
        el.innerHTML = this.golovi_gost;
        tr.appendChild(el);

        var el = document.createElement("td");
        el.innerHTML = this.gost.naziv;
        el.nodeValue = this.gost.naziv;
        tr.appendChild(el);

        var el = document.createElement("td");
        el.innerHTML = this.sudija.ime + " " + this.sudija.prezime;
        tr.appendChild(el);
    }


    
}