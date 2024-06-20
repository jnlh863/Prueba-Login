package com.example.logintest

import com.example.logintest.Responses.CreateProfile
import com.example.logintest.Responses.CreateUser
import com.example.logintest.Responses.GetProfileResponse
import com.example.logintest.Responses.GetUser
import com.example.logintest.Responses.GetUserResponse
import com.example.logintest.Responses.LoginResponse
import com.example.logintest.Responses.MessageResponse
import com.example.logintest.Responses.RestPass
import com.example.logintest.Responses.Token
import com.example.logintest.Responses.UpdateUser
import com.google.gson.GsonBuilder
import okhttp3.OkHttpClient
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.PUT
import retrofit2.http.Path
import java.util.UUID
import java.util.concurrent.TimeUnit
import javax.net.ssl.HostnameVerifier
import javax.net.ssl.SSLContext
import javax.net.ssl.SSLSession
import javax.net.ssl.TrustManager
import javax.net.ssl.X509TrustManager

object AppConstantes{
    const val Base_Url = "http://developertesting.us-east-2.elasticbeanstalk.com/"
}

interface RetrofitService {

    @GET("api/users/{id}")
    suspend fun getUser(
        @Path ("id") id : String
    ): Response<GetUserResponse>

    @POST("api/users")
    suspend fun createUser(
        @Body user: CreateUser
    ): Response<MessageResponse>

    @PUT("api/users/{id}")
    suspend fun updateUser(
        @Path ("id") id: String,
        @Body user: GetUser
    ): Response <MessageResponse>

///////////////////////////////////////////////////////////////////////////////////////////////////////

    @POST("api/auth/login")
    suspend fun loginUser(
        @Body user: UpdateUser
    ): Response<LoginResponse>

    @POST("api/auth/forgot-password")
    suspend fun restPass(
        @Body user: RestPass
    ): Response<MessageResponse>

///////////////////////////////////////////////////////////////////////////////////////////////////////////

    @POST("api/users/profile/{id}")
    suspend fun createProfile(
        @Path ("id") id: String,
        @Body user: CreateProfile
    ): Response<MessageResponse>

    @GET("api/users/profile/{id}")
    suspend fun getProfile(
        @Path ("id") id: String
    ): Response<GetProfileResponse>

    @PUT("api/users/profile/{id}")
    suspend fun updateProfile(
        @Path ("id") id: String,
        @Body user: CreateProfile
    ): Response<MessageResponse>
}

object RetrofitClient {

    private var retrofit: Retrofit? = null

    private val okHttpClient: OkHttpClient by lazy {
        OkHttpClient.Builder()
            .connectTimeout(30, TimeUnit.SECONDS)
            .readTimeout(30, TimeUnit.SECONDS)
            .writeTimeout(30, TimeUnit.SECONDS)
            .build()
    }

    fun getClient(token: String): RetrofitService {
        if (retrofit == null) {
            retrofit = Retrofit.Builder()
                .baseUrl(AppConstantes.Base_Url)
                .client(okHttpClient.newBuilder()
                    .addInterceptor { chain ->
                        val original = chain.request()
                        val requestBuilder = original.newBuilder()
                            .header("Authorization", "Bearer $token")
                        val request = requestBuilder.build()
                        chain.proceed(request)
                    }
                    .build())
                .addConverterFactory(GsonConverterFactory.create())
                .build()
        }
        return retrofit!!.create(RetrofitService::class.java)
    }

    val webService: RetrofitService by lazy {
        Retrofit.Builder()
            .baseUrl(AppConstantes.Base_Url)
            .client(okHttpClient)
            .addConverterFactory(GsonConverterFactory.create())
            .build()
            .create(RetrofitService::class.java)
    }
}
