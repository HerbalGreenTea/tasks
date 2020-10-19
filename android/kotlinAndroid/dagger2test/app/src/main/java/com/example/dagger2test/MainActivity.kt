package com.example.dagger2test

import android.app.Application
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import androidx.fragment.app.Fragment
import com.example.data.repositories.ProfilesRepositoryImpl
import com.example.domain.useCase.LoadProfileUseCase
import com.example.domain.useCase.ProfilesRepository
import dagger.*
import kotlinx.android.extensions.ContainerOptions
import kotlinx.android.synthetic.main.activity_main.*
import javax.inject.*

class MainActivity : AppCompatActivity() {

    lateinit var loadProfileUseCase: LoadProfileUseCase

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        loadProfileUseCase = DaggerApplicationComponent.create().getLoadProfileUseCase()

        name_main.text = loadProfileUseCase.Start()
    }
}

@Module
class ProfilesModule {

    @Provides
    fun provideLoadProfileUseCase(profilesRepository: ProfilesRepositoryImpl): ProfilesRepository {
        return profilesRepository
    }
}

@Singleton
@Component(modules = [ProfilesModule::class])
interface ApplicationComponent {

    fun getLoadProfileUseCase(): LoadProfileUseCase
}

class YourApp: Application() {

    lateinit var applic: ApplicationComponent
        private set

    override fun onCreate() {
        super.onCreate()
        applic = DaggerApplicationComponent.create()
    }
}
