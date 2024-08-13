### Detailed Application Structure for Full Marks

#### 1. **Project Overview**

- **Project Name**: MovieHouse
- **Language**: Java
- **IDE**: Android Studio
- **Package Name**: `com.example.moviehouse`
- **API Used**: OMDB API for fetching movie data
- **Advanced Features**: OMDB API Integration, On-Device Camera Integration, On-Device File Storage
- **UI Structure**: Drawer Layout with multiple fragments and activities

#### 2. **Application Architecture**

The application follows the MVVM (Model-View-ViewModel) pattern to ensure separation of concerns and maintainability.

##### **2.1. Model Layer**

- **User Model**
  - Fields: `user_id`, `username`, `email`, `password`, `profile_picture`
  - Methods: Getters/Setters, `validateUserCredentials`, `saveProfilePicture`
  
- **Movie Model**
  - Fields: `movie_id`, `title`, `genre`, `language`, `rating`, `poster_url`, `description`
  - Methods: Getters/Setters, `fetchMovieDetailsFromOMDB`, `getGenreName`, `getLanguageName`
  
- **Review Model**
  - Fields: `review_id`, `movie_id`, `user_id`, `rating`, `review_text`, `review_image`
  - Methods: Getters/Setters, `saveReviewImage`, `getUserReview`, `getMovieReviews`

- **Watchlist Model**
  - Fields: `watchlist_id`, `user_id`, `movie_id`
  - Methods: Getters/Setters, `addToWatchlist`, `removeFromWatchlist`, `getUserWatchlist`

##### **2.2. ViewModel Layer**

- **UserViewModel**
  - Methods: `loginUser`, `registerUser`, `updateProfilePicture`, `getUserDetails`
  - LiveData: `userLiveData`, `profilePictureLiveData`

- **MovieViewModel**
  - Methods: `searchMovies`, `fetchMovieDetails`, `addMovieToWatchlist`, `removeMovieFromWatchlist`
  - LiveData: `moviesLiveData`, `movieDetailsLiveData`

- **ReviewViewModel**
  - Methods: `submitReview`, `fetchMovieReviews`, `getUserReviews`
  - LiveData: `reviewsLiveData`, `userReviewsLiveData`

- **WatchlistViewModel**
  - Methods: `getUserWatchlist`
  - LiveData: `watchlistLiveData`

##### **2.3. View Layer**

- **MainActivity**
  - Layout: DrawerLayout with navigation menu
  - Fragments: `HomeFragment`, `SearchFragment`, `WatchlistFragment`, `ProfileFragment`, `AboutFragment`
  
- **HomeFragment**
  - Displays a list of trending movies (fetched from OMDB API).
  - Layout: RecyclerView for movie list, SearchBar, and Buttons for navigation.
  
- **SearchFragment**
  - Provides search functionality to look up movies by title or genre.
  - Layout: SearchView, RecyclerView for search results.
  
- **MovieDetailsActivity**
  - Displays detailed information about a selected movie.
  - Layout: ImageView for poster, TextViews for title, genre, rating, description, and Button for adding to Watchlist.
  
- **WatchlistFragment**
  - Displays movies added to the user's watchlist.
  - Layout: RecyclerView for displaying watchlist movies.
  
- **ProfileFragment**
  - Displays and allows editing of user profile information.
  - Layout: EditTexts for username, email, profile picture, and Button for updating profile.
  
- **AboutFragment**
  - Displays information about the app and developers.
  - Layout: TextViews for developer names, app version, and contact information.
  
- **ReviewActivity**
  - Allows users to submit reviews for movies.
  - Layout: EditText for review text, RatingBar for rating, ImageView for adding a review image, and Button for submitting.

#### 3. **Database Structure**

The app uses a local SQLite database for persistent storage, ensuring that all CRUD operations are handled efficiently.

- **Users Table**
  - Columns: `user_id` (PK), `username`, `email`, `password`, `profile_picture`
  - Operations: Insert, Update, Delete, Select, Select All

- **Movies Table**
  - Columns: `movie_id` (PK), `title`, `genre`, `language`, `rating`, `poster_url`, `description`
  - Operations: Insert, Update, Delete, Select, Select All

- **Reviews Table**
  - Columns: `review_id` (PK), `movie_id` (FK), `user_id` (FK), `rating`, `review_text`, `review_image`
  - Operations: Insert, Update, Delete, Select, Select All

- **Watchlist Table**
  - Columns: `watchlist_id` (PK), `user_id` (FK), `movie_id` (FK)
  - Operations: Insert, Delete, Select All

- **Genres Table (Lookup)**
  - Columns: `genre_id` (PK), `genre_name`
  - Operations: Insert, Select All

- **MovieLanguages Table (Lookup)**
  - Columns: `language_id` (PK), `language_name`
  - Operations: Insert, Select All

#### 4. **Advanced Features Implementation**

1. **OMDB API Integration**
   - Utilizes Retrofit to fetch movie data.
   - OMDB API responses are parsed and stored in the local database.

2. **On-Device Camera Integration**
   - Allows users to take a profile picture and attach images to reviews.
   - Camera functionality is implemented using Android’s CameraX API.

3. **On-Device File Storage**
   - User-generated content like review images are stored on the device using Android’s internal storage mechanisms.

#### 5. **Coding Practices**

- **Comments**: Comprehensive comments explaining complex logic and functions.
- **Variable Naming**: Follows Android best practices with clear and descriptive names.
- **Methods**: Modular code with reusable methods to prevent code duplication.
- **ViewBinding**: Enabled to simplify UI interaction.

#### 6. **UI Design**

- **Consistency**: Colors, fonts, and graphics follow a consistent theme.
- **Spacing**: Adequate padding and margins are used to avoid clutter.
- **Visual Appeal**: Modern and attractive UI with intuitive navigation.

#### 7. **Submission Requirements**

- **Zip File**: Contains the entire project including all necessary assets, Java files, and resource files.
- **Word Document**: Includes all Java code with meaningful comments and relevant screenshots.
- **Video Demo**: A recorded demo showcasing all features, uploaded to eConestoga or OneDrive.

This structure ensures that your project is comprehensive, adheres to the assignment requirements, and is presented in a way that highlights its complexity and advanced features, maximizing your chances of getting full marks.

```markup

MovieHouse/
│
├── app/
│   ├── build/
│   ├── libs/
│   ├── src/
│   │   ├── androidTest/
│   │   │   └── java/
│   │   │       └── com/example/moviehouse/
│   │   ├── main/
│   │   │   ├── java/
│   │   │   │   └── com/example/moviehouse/
│   │   │   │       ├── model/
│   │   │   │       │   ├── User.java
│   │   │   │       │   ├── Movie.java
│   │   │   │       │   ├── Review.java
│   │   │   │       │   ├── Watchlist.java
│   │   │   │       │   └── LookupTables/
│   │   │   │       │       ├── Genre.java
│   │   │   │       │       └── MovieLanguage.java
│   │   │   │       ├── view/
│   │   │   │       │   ├── MainActivity.java
│   │   │   │       │   ├── HomeFragment.java
│   │   │   │       │   ├── SearchFragment.java
│   │   │   │       │   ├── MovieDetailsActivity.java
│   │   │   │       │   ├── WatchlistFragment.java
│   │   │   │       │   ├── ProfileFragment.java
│   │   │   │       │   ├── AboutFragment.java
│   │   │   │       │   └── ReviewActivity.java
│   │   │   │       ├── viewmodel/
│   │   │   │       │   ├── UserViewModel.java
│   │   │   │       │   ├── MovieViewModel.java
│   │   │   │       │   ├── ReviewViewModel.java
│   │   │   │       │   └── WatchlistViewModel.java
│   │   │   │       ├── database/
│   │   │   │       │   ├── MovieHouseDatabase.java
│   │   │   │       │   ├── UserDao.java
│   │   │   │       │   ├── MovieDao.java
│   │   │   │       │   ├── ReviewDao.java
│   │   │   │       │   └── WatchlistDao.java
│   │   │   │       ├── repository/
│   │   │   │       │   ├── UserRepository.java
│   │   │   │       │   ├── MovieRepository.java
│   │   │   │       │   ├── ReviewRepository.java
│   │   │   │       │   └── WatchlistRepository.java
│   │   │   │       ├── network/
│   │   │   │       │   ├── OMDBApiService.java
│   │   │   │       │   └── RetrofitClient.java
│   │   │   │       ├── util/
│   │   │   │       │   ├── CameraUtils.java
│   │   │   │       │   ├── FileUtils.java
│   │   │   │       │   └── ViewBindingAdapter.java
│   │   │   │       └── MovieHouseApp.java
│   │   │   ├── res/
│   │   │   │   ├── drawable/
│   │   │   │   ├── layout/
│   │   │   │   │   ├── activity_main.xml
│   │   │   │   │   ├── fragment_home.xml
│   │   │   │   │   ├── fragment_search.xml
│   │   │   │   │   ├── activity_movie_details.xml
│   │   │   │   │   ├── fragment_watchlist.xml
│   │   │   │   │   ├── fragment_profile.xml
│   │   │   │   │   ├── fragment_about.xml
│   │   │   │   │   └── activity_review.xml
│   │   │   │   ├── values/
│   │   │   │   │   ├── colors.xml
│   │   │   │   │   ├── strings.xml
│   │   │   │   │   ├── dimens.xml
│   │   │   │   │   └── styles.xml
│   │   │   ├── AndroidManifest.xml
│   │   ├── test/
│   │   │   └── java/
│   │   │       └── com/example/moviehouse/
│   ├── build.gradle
│   └── proguard-rules.pro
│
├── gradle/
│   └── wrapper/
│       ├── gradle-wrapper.properties
│       └── gradle-wrapper.jar
├── .gitignore
├── build.gradle
├── gradle.properties
├── gradlew
├── gradlew.bat
└── settings.gradle
```