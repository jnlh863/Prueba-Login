package com.example.logintest

import com.example.logintest.Responses.CreateProfile
import com.example.logintest.Responses.CreateUser
import com.example.logintest.Responses.GetUser
import com.example.logintest.Responses.RestPass
import com.example.logintest.Responses.Token
import com.example.logintest.Responses.UpdateUser
import com.google.gson.GsonBuilder
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.PUT
import retrofit2.http.Path

object AppConstantes{
    const val Base_Url = "https://localhost:7039/api"
}

interface RetrofitService {

    @GET("/users/{id}")
    suspend fun getUser(
        @Path ("id") id : String
    ): Response<GetUser>

    @POST("/users")
    suspend fun createUser(
        @Body user: CreateUser
    ): Response<String>

    @PUT("/users/{id}")
    suspend fun updateUser(
        @Path ("id") id: String,
        @Body user: GetUser
    ): Response <String>

///////////////////////////////////////////////////////////////////////////////////////////////////////

    @POST("/auth/login")
    suspend fun loginUser(
        @Body user: UpdateUser
    ): Response<Token>

    @POST("/auth/forgot-password")
    suspend fun restPass(
        @Body user: RestPass
    ): Response<String>

///////////////////////////////////////////////////////////////////////////////////////////////////////////

    @POST("users/profile/{id}")
    suspend fun createProfile(
        @Path ("id") id: String,
        @Body user: CreateProfile
    ): Response<String>

    @GET("users/profile/{id}")
    suspend fun getProfile(
        @Path ("id") id: String
    ): Response<CreateProfile>

    @PUT("users/profile/{id}")
    suspend fun updateProfile(
        @Path ("id") id: String,
        @Body user: CreateProfile
    ): Response<String>
}

object RetrofitClient{
    val webService: RetrofitService by lazy {
        Retrofit.Builder()
            .baseUrl(AppConstantes.Base_Url)
            .addConverterFactory(GsonConverterFactory.create(GsonBuilder().create()))
            .build().create(RetrofitService::class.java)
    }
}

