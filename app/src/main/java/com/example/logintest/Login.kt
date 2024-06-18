package com.example.logintest

import android.content.Intent
import android.content.SharedPreferences
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import com.example.logintest.Responses.UpdateUser
import android.os.Bundle
import android.view.View
import android.widget.Toast
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

class Login : AppCompatActivity(){

    lateinit var login : Button
    lateinit var register : Button
    lateinit var correo : EditText
    lateinit var password : EditText
    lateinit var resetpass : TextView
    lateinit var preferences : SharedPreferences
    var LOG = UpdateUser("","")
    var band = true;

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.login)

        correo = findViewById(R.id.email_input)
        password = findViewById(R.id.password_input)
        login = findViewById(R.id.login_btn)
        register = findViewById(R.id.register_btn)
        resetpass = findViewById(R.id.resetPass)

        preferences = getSharedPreferences("Preferences", MODE_PRIVATE)

        login.setOnClickListener(View.OnClickListener {
            val correo = correo.getText().toString().trim { it <= ' ' }
            val password = password.getText().toString().trim { it <= ' ' }
            if (correo.isEmpty() && password.isEmpty()) {
                Toast.makeText(
                    this@Login,
                    "Complete los campos correspondientes",
                    Toast.LENGTH_SHORT
                ).show()
            } else {
                InicioSesionU(correo, password)
            }
        })
        register.setOnClickListener(View.OnClickListener {
            startActivity(
                Intent(
                    this@Login,
                    Register::class.java
                )
            )
        })

        resetpass.setOnClickListener(View.OnClickListener {
            startActivity(
                Intent(
                    this@Login,
                    ResetPass::class.java
                )
            )
        })


    }

    private fun InicioSesionU(correo: String, password: String) {
        this.LOG.email = correo
        this.LOG.password = password

        if (band) {
            CoroutineScope(Dispatchers.IO).launch {

                val call = RetrofitClient.webService.loginUser(LOG)
                val response = call.body()

                runOnUiThread {
                    if (response != null && response.token.isNotEmpty()) {
                        val editor = preferences.edit()
                        editor.putString("token", response.token)
                        editor.putString("id", response.id)
                        editor.commit()

                        Toast.makeText(this@Login, "Bienvenido", Toast.LENGTH_SHORT).show()
                        limpiarObjeto()

                        val intent = Intent(this@Login, MainActivity::class.java)
                        startActivity(intent)

                        limpiarcampos()

                        finish()

                    } else {
                        Toast.makeText(
                            this@Login,
                            "Error de autenticación",
                            Toast.LENGTH_SHORT
                        ).show()
                    }
                }
            }
        }
    }

    private fun limpiarcampos() {
        correo.setText("")
        password.setText("")
    }

    private fun limpiarObjeto() {
        this.LOG.email = ""
        this.LOG.password = ""
    }


}