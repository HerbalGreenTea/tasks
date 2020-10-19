package com.example.modules

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.example.domain.TestClass
import com.example.repos.ClassDomain
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        main_res.text = ClassDomain.test
    }
}