# CocktailBar - Application Blazor Server

**Cours** : Programmation Orientée Objet (5IOBJ-1) — IFOSUP Wavre
**Professeur** : Jérémy Kairis

## Description

CocktailBar est une application web développée en Blazor Server (.NET 9) qui permet de parcourir, rechercher et découvrir des cocktails via l'API publique [TheCocktailDB](https://www.thecocktaildb.com/). Un utilisateur authentifié peut sauvegarder ses cocktails favoris, les personnaliser (nom custom, notes, notation par étoiles) et gérer sa liste de favoris.

### Fonctionnalités principales

- Recherche de cocktails par nom
- Filtrage par catégorie (Ordinary Drink, Cocktail, Shot, etc.)
- Cocktail aléatoire ("Cocktail Surprise")
- Page de détail avec ingrédients, mesures et instructions
- Système de favoris avec édition inline (nom personnalisé, notes, notation 1-5 étoiles)
- Authentification simple en mémoire (3 comptes de test)

## Architecture

### Pourquoi Blazor Server ?

Blazor Server a été choisi plutôt que Blazor WebAssembly pour plusieurs raisons :

1. **Simplicité de déploiement** : pas besoin de télécharger le runtime .NET côté client
2. **Accès direct aux services serveur** : les services Singleton partagent l'état entre les composants sans avoir besoin d'une API intermédiaire
3. **Temps de chargement initial rapide** : pas de téléchargement de DLL côté client
4. **Adapté au contexte du cours** : une seule application, pas de séparation client/serveur à gérer

### Pourquoi des services Singleton ?

Les services `FavoriteService` et `AuthService` sont enregistrés en Singleton car :

- L'application n'utilise pas de base de données : les données sont stockées **en mémoire**
- En Singleton, l'état persiste pendant toute la durée de vie de l'application
- Cela simule un "stockage serveur" simple, suffisant pour une démo/POC
- **Limite connue** : en Singleton, tous les utilisateurs partagent les mêmes favoris. Pour une vraie app, on utiliserait Scoped + base de données

### Pourquoi System.Text.Json ?

- Inclus nativement dans .NET (pas de dépendance externe comme Newtonsoft.Json)
- Performant et léger
- Les attributs `[JsonPropertyName]` permettent de mapper les noms de propriétés de l'API (snake_case/camelCase) aux propriétés C# (PascalCase)

## API choisie : TheCocktailDB

- **Gratuite** : la clé "1" est incluse dans l'URL, aucune inscription requise
- **Sans authentification** : pas de token, pas d'OAuth, simplifie le développement
- **Données riches** : nom, catégorie, type (alcoholic/non-alcoholic), verre, ingrédients (x15), mesures, instructions
- **Images incluses** : photos de cocktails + images de chaque ingrédient
- **Plusieurs endpoints** : recherche, lookup, filtre, aléatoire, liste de catégories

## Structure des dossiers

```
CocktailBar/                          ← Blazor Server app (.NET 9)
│
├── Models/
│   ├── Cocktail.cs                   ← Classes de désérialisation JSON (CocktailApiResponse, CocktailDetail, etc.)
│   └── Favorite.cs                   ← Modèle favori avec nom custom, notes, rating
│
├── Services/
│   ├── CocktailService.cs            ← Appels HTTP vers TheCocktailDB
│   ├── FavoriteService.cs            ← CRUD favoris en mémoire + événement OnChange
│   └── AuthService.cs                ← Auth simple avec 3 comptes en dur
│
├── Components/
│   ├── App.razor                     ← Composant racine HTML
│   ├── Routes.razor                  ← Configuration du routeur
│   ├── _Imports.razor                ← Imports globaux
│   ├── Layout/
│   │   ├── MainLayout.razor          ← Layout avec sidebar + contenu principal
│   │   └── NavMenu.razor             ← Navigation avec badges et état auth
│   └── Pages/
│       ├── Home.razor                ← Page d'accueil : recherche, filtres, grille
│       ├── CocktailDetail.razor      ← Détail cocktail : image, ingrédients, favoris
│       ├── Favorites.razor           ← Liste favoris avec édition inline
│       └── Login.razor               ← Formulaire de connexion
│
├── wwwroot/
│   └── app.css                       ← Thème sombre bar/cocktail
│
├── Properties/
│   └── launchSettings.json
├── appsettings.json
├── Program.cs                        ← Configuration DI et pipeline
└── CocktailBar.csproj

CocktailBar.Tests/                    ← Projet de tests xUnit + bUnit
│
├── FavoriteServiceTests.cs           ← 6 tests unitaires FavoriteService
├── AuthServiceTests.cs               ← 4 tests unitaires AuthService
├── CocktailDetailTests.cs            ← 3 tests d'intégration bUnit
└── CocktailBar.Tests.csproj
```

## Comment lancer le projet

```bash
cd CocktailBar
dotnet run
# Ouvrir https://localhost:5001
```

## Comment lancer les tests

```bash
cd CocktailBar.Tests
dotnet test
```

## Comptes de test

| Utilisateur | Mot de passe |
|-------------|-------------|
| james       | bond007     |
| martini     | shaken      |
| bar         | tender      |

## Choix techniques justifiés

### Singleton vs Scoped

- **Singleton** : utilisé pour `FavoriteService` et `AuthService` car les données sont en mémoire et doivent persister entre les requêtes. Pas de base de données donc pas besoin de Scoped.
- **`AddHttpClient<CocktailService>()`** : crée un HttpClient typé avec gestion automatique du cycle de vie (évite les problèmes de socket exhaustion). Le service lui-même est Transient (par défaut avec AddHttpClient).

### Pattern AAA dans les tests

Chaque test suit le pattern **Arrange / Act / Assert** avec des commentaires explicites :
- **Arrange** : préparation des données et instanciation du service
- **Act** : appel de la méthode testée
- **Assert** : vérification du résultat attendu

Chaque test instancie son propre service pour garantir l'indépendance totale entre les tests.

### bUnit pour les tests Blazor

bUnit permet de tester les composants Razor en isolation, sans navigateur. On peut :
- Injecter des services mockés/stubbed
- Rendre un composant avec des paramètres
- Vérifier le markup HTML généré
- Simuler des interactions utilisateur


### Règles de commit utilisées

- Format : `type(scope): description en minuscules`
- `feat` : nouvelle fonctionnalité
- `fix` : correction de bug
- `style` : CSS / mise en forme
- `test` : ajout ou modification de tests
- `docs` : documentation
- Description courte (max 60 chars), au présent, sans majuscule, sans point final
