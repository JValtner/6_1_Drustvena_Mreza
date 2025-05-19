
'use strict'
class Korisnik {
    constructor(id, korisnickoIme, ime, prezime, datumRodjenja,grupeKorisnika) {
        this.id = id
        this.korisnickoIme = korisnickoIme
        this.ime = ime
        this.prezime = prezime
        this.datumRodjenja = datumRodjenja
        this.grupeKorisnika = grupeKorisnika
    }
}
class Grupa{
    constructor(id, ime,datumOsnivanja){
        this.id = id
        this.ime = ime
        this.datumOsnivanja = datumOsnivanja
    }
}


document.addEventListener('DOMContentLoaded', initializeKorisnikaGrupe)

function initializeKorisnikaGrupe() {
    let korisniciGrupe = []
    
    korisniciGrupe = loadKorisnikaGrupe()
    
    saveLocalStorage(korisniciGrupe)
}

function saveLocalStorage(korisniciGrupe) {
    let korisniciGrupeJSON = JSON.stringify(korisniciGrupe)
    localStorage.setItem("korisnici", korisniciGrupeJSON)
}

function formatDate(isoDateString) {
    const date = new Date(isoDateString)
    return date.toLocaleDateString('sr-RS')
}
