package com.example.logintest

import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import android.view.View
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.example.logintest.Responses.CreateUser
import com.example.logintest.Responses.UpdateUser
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

class Register : AppCompatActivity() {

    lateinit var register: Button
    lateinit var username: EditText
    lateinit var correo: EditText
    lateinit var password: EditText
    lateinit var confirmpass: EditText
    var REG = CreateUser("", "", "")
    var band = true;

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.register)

        username = findViewById(R.id.name_input)
        correo = findViewById(R.id.email_input)
        password = findViewById(R.id.password_input)
        confirmpass = findViewById(R.id.cpass_input)
        register = findViewById(R.id.register_btn)

        register.setOnClickListener(View.OnClickListener {
            val user = username.getText().toString().trim { it <= ' ' }
            val email = correo.getText().toString().trim { it <= ' ' }
            val pass = password.getText().toString().trim { it <= ' ' }
            val cpass = confirmpass.getText().toString().trim { it <= ' ' }


            if (user.isEmpty()){
                Toast.makeText(
                    this@Register,
                    "Complete los campos correspondientes",
                    Toast.LENGTH_SHORT
                ).show();
            }

            if (email.isEmpty()){
                Toast.makeText(
                    this@Register,
                    "Complete los campos correspondientes",
                    Toast.LENGTH_SHORT
                ).show();
            }

            if (pass.isEmpty()){
                Toast.makeText(
                    this@Register,
                    "Complete los campos correspondientes",
                    Toast.LENGTH_SHORT
                ).show();
            }

            if (cpass.isEmpty()){
                Toast.makeText(
                    this@Register,
                    "Complete los campos correspondientes",
                    Toast.LENGTH_SHORT
                ).show();
            }

            if(pass != cpass) {
                Toast.makeText(
                    this@Register,
                    "La contraseña no corresponde a la que coloco anteriormente",
                    Toast.LENGTH_SHORT
                ).show();
            }

            createUser(user, email, cpass)

        })

    }

    private fun createUser(user: String, email: String, cpass: String) {
        this.REG.username = user
        this.REG.email = email
        this.REG.password = cpass

        if (band) {
            CoroutineScope(Dispatchers.IO).launch {
                val call = RetrofitClient.webService.createUser(REG)
                val response = call.body()

                runOnUiThread {
                    if (response != null) {
                        if (response.response == "SI") {

                            Toast.makeText(this@Register, "Bienvenido", Toast.LENGTH_SHORT).show()
                            limpiarObjeto()

                            val intent = Intent(this@Register, Login::class.java)
                            startActivity(intent)

                            limpiarcampos()

                            finish()

                        }
                    }else {
                        Toast.makeText(
                            this@Register,
                            "Error de al guardar informacion, intentelo de nuevo",
                            Toast.LENGTH_SHORT
                        ).show()
                    }
                }
            }
        }

    }
    private fun limpiarcampos() {
        username.setText("")
        correo.setText("")
        password.setText("")
    }

    private fun limpiarObjeto() {
        this.REG.username = ""
        this.REG.email = ""
        this.REG.email = ""
    }
}