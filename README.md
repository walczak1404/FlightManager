
# FlightManager

## Spis treści
1. [ Opis projektu ](#Opis-projektu)
2. [Funkcjonalności](#Funkcjonalności)
3. [ Wykorzystane technologie ](#Wykorzystane-technologie)
4. [ Wymagania ](#Wymagania)
5. [ Instrukcje uruchomienia API](#Instrukcje-uruchomienia-API)
6. [ Instrukcje uruchomienia interfejsu użytkownika](#Instrukcje-uruchomienia-interfejsu-użytkownika)


## Opis projektu

FlightManager to aplikacja stworzona z myślą o zarządzaniu lotami. Umożliwia ona użytkownikom przeglądanie dostępnych lotów, a po autoryzacji również dodawanie, modyfikowanie oraz usuwanie. Loty można również filtrować po miejscach wylotu i przylotu oraz sortować.

## Funkcjonalności
- Logowanie oraz rejestracja
- Przeglądanie dostępnych lotów
- Sortowanie dostępnych lotów
- Filtrowanie dostępnych lotów
- Dodawanie lotów (po zalogowaniu)
- Usuwanie lotów (po zalogowaniu)
- Modyfikowanie lotów (po zalogowaniu)

<a name="technologies"></a>
## Wykorzystane technologie

Aplikacja FlightManager została zbudowana przy użyciu następujących technologii:

- **REST API**:
  - .Net Core 7
  - Entity Framework
  - Identity Framework

- **Frontend**:
  - HTML5
  - CSS3
  - TypeScript
  - Angular

- **Testy**:
	- XUnit
	- Moq
	- FluentAssertions
	- AutoFixture

## Wymagania

Aby uruchomić aplikację lokalnie, wymagane są następujące komponenty:

- Visual Studio 2022
- node.js
- SqlServer

## Instrukcje uruchomienia API

1. Sklonuj repozytorium.
2. Zainstaluj potrzebne paczki w razie konieczności:
	- Dla projektu .Core:
		- Microsoft.AspNetCore.Identity.EntityFramework 7.0.18
		- System.IdentityModel.Tokens.Jwt 7.5.1
	- Dla projektu .Infrastructure:
		- Microsoft.EntityFrameworkCore.SqlServer 7.0.18
		- Microsoft.EntityFrameworkCore.Tools 7.0.18
	- Dla projektu .Web:
		- Microsoft.AspNetCore.Authentication.JwtBearer 7.0.18
		- Microsoft.AspNetCore.Identity.EntityFrameworkCore 7.0.18
		- Microsoft.AspNetCore.OpenApi 7.0.18
		- Microsoft.EntityFrameworkCore 7.0.18
		- Microsoft.EntityFrameworkCore.Design 7.0.18
		- Swashbuckle.AspNetCore 6.5.0
	- Dla projektu .ServiceTests:
		- AutoFixture 4.18.1
		- coverlet.collector 3.2.0
		- FluentAssertions 6.12.0
		- Microsoft.NET.Test.Sdk 17.7.1
		- Moq 4.20.70
		- xunit 2.4.2
		- xunit.runner.visualstudio 2.4.5
3. Stwórz pustą bazę danych za pomocą wbudowanego w Visual Studio SQL Server Object Explorer
4. Dodaj do konfiguracji następujące właściwości:
	- "ConnectionStrings": {
		- "DefaultConnection": connection string do stworzonej wcześniej bazy danych
	} - potrzebne do utworzenia bazy danych na podstawie migracji
	- "JWT": {
		- "Audience": domena aplikacji klienta
		- "Issuer": domena API
		- "Key": dowolny klucz używany do generowania tokena uwierzytelniającego
		- "Expiration_Minutes": czas, po którym wygasa token uwierzytelniający
	   } - są to rzeczy potrzebne do poprawnego generowania tokena uwierzytelniającego. Moje są zawarte w secrets managerze, więc nie są udostępnione w repozytorium.
5. W Package Manager Console na projekcie .Infrastructure przeprowadź migrację za pomocą polecenia update-database.
6. API jest gotowe do uruchomienia. jego dokumentacja będzie dostępna po uruchomieniu w przeglądarce

## Instrukcje uruchomienia interfejsu użytkownika

1. Wejdź w katalog aplikacji klienta.
2. Zainstaluj wszystkie używane paczki za pomocą komendy npm install w terminalu.
3. Uruchom aplikację za pomocą komendy ng serve.
4. **Do poprawngo działania aplikacji klienta wymagane jest, aby API działało na porcie 7074**
