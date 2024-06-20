package com.example.logintest

import android.content.Context
import android.content.Context.MODE_PRIVATE
import android.content.Intent
import android.icu.text.SimpleDateFormat
import android.icu.util.Calendar
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import android.widget.Toast
import androidx.fragment.app.Fragment
import com.example.logintest.Responses.CreateProfile
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import java.util.Date
import java.util.UUID

class Profile : Fragment() {

    var PROF = CreateProfile("", "", 0, 0, "")
    var band = true;
    private lateinit var iU: TextView
    private lateinit var s: EditText
    private lateinit var est: EditText
    private lateinit var pe: EditText
    private lateinit var prot: EditText

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.fragment_profile, container, false)

        iU = view.findViewById<TextView>(R.id.info_user)
        s = view.findViewById<EditText>(R.id.sex)
        est = view.findViewById<EditText>(R.id.stature)
        pe = view.findViewById<EditText>(R.id.weight)
        prot = view.findViewById<EditText>(R.id.protocol)

        return view
    }

    override fun onResume() {
        super.onResume()
        val preferences = requireContext().getSharedPreferences("Preferences", Context.MODE_PRIVATE)
        val id = preferences.getString("id", null).toString()
        val token = preferences.getString("token", null).toString()
        infoUser(id, token)
        infoProfile(id, token)
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?){
        super.onViewCreated(view, savedInstanceState)

        var edittextsexo = view.findViewById<EditText>(R.id.sex)
        var edittextestatura = view.findViewById<EditText>(R.id.stature)
        var edittextpeso = view.findViewById<EditText>(R.id.weight)
        var edittextprotocolo = view.findViewById<EditText>(R.id.protocol)
        var btnUpdate = view.findViewById<Button>(R.id.updatebtn)
        val preferences = requireContext().getSharedPreferences("Preferences", Context.MODE_PRIVATE)


        btnUpdate.setOnClickListener{
            val token = preferences.getString("token", null).toString()
            val id = preferences.getString("id", null).toString()
            val sexo = edittextsexo.text.toString()
            val estatura = edittextestatura.text.toString().toIntOrNull() ?: 0
            val peso = edittextpeso.text.toString().toIntOrNull() ?: 0
            val protocolo = edittextprotocolo.text.toString()
            val validsex = arrayOf("Masculino", "Femenino")
            val validProtocols = arrayOf("Diabetes", "Obesidad")

            var errorMessage: String? = null

            when {
                sexo.isEmpty() -> {
                    errorMessage = "Complete los campos correspondientes"
                }
                !validsex.contains(sexo) -> {
                    errorMessage = "Sexo inválido. Solo se permiten 'Masculino' o 'Femenino'"
                }
                estatura == null -> {
                    errorMessage = "Complete los campos correspondientes"
                }
                peso == null -> {
                    errorMessage = "Complete los campos correspondientes"
                }
                !validProtocols.contains(protocolo) -> {
                    errorMessage = "Protocolo inválido. Debe ser uno de los siguientes: ${validProtocols.joinToString(", ")}"
                }
                id == null -> {
                    errorMessage = "Hubo un problema en guardar la información"
                }
            }

            if (errorMessage != null) {
                Toast.makeText(requireContext(), errorMessage, Toast.LENGTH_SHORT).show()
            } else {
                updateProfile(id, token, sexo, estatura, peso, protocolo)
            }
        }

    }

    private fun infoUser(id: String, token: String) {
        if (band) {
            CoroutineScope(Dispatchers.IO).launch {
                val validation = RetrofitClient.getClient(token)
                val call = validation.getUser(id)
                val response = call.body()

                activity?.runOnUiThread{
                    if (response != null) {
                        iU.text = "ID: ${response.response.id} \n\nUsername: ${response.response.username}\n\nEmail: ${response.response.email}"
                    } else {
                        Toast.makeText(
                            requireContext(),
                            "User not found. Try again",
                            Toast.LENGTH_SHORT
                        ).show()
                        iU.text = "Hubo un error aqui"
                        Log.d("RESPONSE", response.toString())
                        Log.d("RESPONSE", id)
                    }
                }
            }
        }
    }

    private fun infoProfile(id: String, token: String) {
        if (band) {
            CoroutineScope(Dispatchers.IO).launch {
                val validation = RetrofitClient.getClient(token)
                val call = validation.getProfile(id)
                val response = call.body()

                activity?.runOnUiThread{
                    if (response != null) {

                        s.hint = response.response.sex
                        est.hint = response.response.stature.toString()
                        pe.hint = response.response.weight.toString()
                        prot.hint = response.response.protocol

                    } else {
                        Toast.makeText(
                            requireContext(),
                            "User not found. Try again",
                            Toast.LENGTH_SHORT
                        ).show()
                        iU.text = "Hubo un error aqui"
                        Log.d("RESPONSE", response.toString())
                        Log.d("RESPONSE", id)
                    }
                }
            }
        }
    }


    private fun updateProfile(id: String, token: String, sexo : String, estatura : Int, peso : Int, protocolo : String) {
        this.PROF.id = id
        this.PROF.sex = sexo
        this.PROF.stature = estatura
        this.PROF.weight = peso
        this.PROF.protocol = protocolo

        if (band) {
            CoroutineScope(Dispatchers.IO).launch {
                val validation = RetrofitClient.getClient(token)
                val call = validation.updateProfile(id, PROF)
                val response = call.body()

                activity?.runOnUiThread{
                    if (response != null) {
                        if (response.response == "Update completed") {

                            Toast.makeText(requireContext(), "Cambios guardados", Toast.LENGTH_SHORT).show()

                        }
                    }else {
                        createProfile(id, token, sexo, estatura, peso, protocolo)
                    }
                }
            }
        }

    }

    private fun createProfile(id: String, token: String, sexo: String, estatura: Int, peso: Int, protocolo: String) {
        this.PROF.id = id
        this.PROF.sex = sexo
        this.PROF.stature = estatura
        this.PROF.weight = peso
        this.PROF.protocol = protocolo

        if (band) {
            CoroutineScope(Dispatchers.IO).launch {
                val validation = RetrofitClient.getClient(token)
                val call = validation.createProfile(id, PROF)
                val response = call.body()

                activity?.runOnUiThread{
                    if (response != null) {
                        if (response.response == "Cambios guardados") {

                            Toast.makeText(requireContext(), "Cambios guardados", Toast.LENGTH_SHORT).show()

                        } else {
                            Toast.makeText(
                                requireContext(),
                                "Try again",
                                Toast.LENGTH_SHORT
                            ).show()
                        }
                    }
                }
            }
        }
    }
}