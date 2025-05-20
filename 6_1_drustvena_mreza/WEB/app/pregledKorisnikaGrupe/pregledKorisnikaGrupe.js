
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
    constructor(id,ime,datumOsnivanja){
        this.id =id
        this.ime = ime
        this.datumOsnivanja = datumOsnivanja
    }
}

const urlParams = new URLSearchParams(window.location.search)
const grupaId = urlParams.get('grupaId') // Preuzimamo vrednost grupaId parametra upita

function initializeKorisnike() {
    
    let korisniciGrupe = loadKorisnikeGrupe()
}

function loadKorisnikeGrupe(){
  fetch('http://localhost:14117/api/korisnik') 
    .then(response => {
      if (!response.ok) {
        throw new Error('Request failed. Status: ' + response.status)
      }
      return response.json()
    })
    .then(korisniciGrupe => createDataTable(korisniciGrupe))  
    .catch(error => {                  
      console.error('Error:', error.message)
      alert('An error occurred while loading the data. Please try again.')
    })
}
function createDataTable(korisniciGrupe) {
    let container = document.querySelector(".main-content") 
    container.innerHTML = `
        <p> Korisnici grupe: </p>
        <th>
        <table class="user-data">
            <thead class="user-data-head">
                <tr>
                    <th>Id</th> 
                    <th>Korisničko Ime</th> 
                    <th>Ime</th> 
                    <th>Prezime</th> 
                    <th>Datum rodjenja</th> 
                </tr>
            </thead>
            <tbody id="user-data-body">
            </tbody>
        </table>
    `

    const tbody = container.querySelector("#user-data-body")

    for (let korisnik of korisniciGrupe) {
        const row = document.createElement("tr")
        row.innerHTML = `
            <td>${korisnik.id}</td>
            <td>${korisnik.korisnickoIme}</td>
            <td>${korisnik.ime}</td>
            <td>${korisnik.prezime}</td>
            <td>${formatDate(korisnik.datumRodjenja)}</td>
        tbody.appendChild(row)
    }
}
function formatDate(isoDateString) {
    const date = new Date(isoDateString)
    return date.toLocaleDateString('sr-RS')
}


function showSuccess() {
    let successMsg = document.querySelector("#success-msg")
    successMsg.style.opacity = "1"
    successMsg.style.color = "green"
    successMsg.style.fontWeight = "bold"

    setTimeout(() => {
        successMsg.style.opacity = "0"
    }, 3000)
}

document.addEventListener('DOMContentLoaded', initializeKorisnike)