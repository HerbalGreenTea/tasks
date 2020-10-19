package com.example.dagger2tes

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import dagger.Binds
import dagger.Component
import dagger.Module
import dagger.Provides
import kotlinx.android.synthetic.main.activity_main.*
import javax.inject.Inject

class MainActivity : AppCompatActivity() {

    @Inject
    lateinit var car: Car

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        var carComponent = DaggerCarComponent.create()
        carComponent.inject(this)

        name.text = TestCar().test()
    }
}

class TestCar {

    @Inject
    lateinit var car: Car

    fun test(): String {
        return car.Start()
    }
}


class Car {

    @Inject
    lateinit var wheels: Wheels

    lateinit var engine: Engine

    @Inject
    constructor(engine: Engine) {
        this.engine = engine
    }

    fun Start(): String {
        return "car new test " + wheels.testStr
    }

    @Inject
    fun provideCarToRemote(remote: Remote) {
        remote.provideCar(this)
    }
}

class Remote {
    lateinit var car: Car

    @Inject
    constructor() {

    }

    fun provideCar(car: Car) {
        this.car = car
    }
}

class Wheels {

    lateinit var rims: Rims
    lateinit var tires: Tires

    constructor(rims: Rims, tires: Tires) {
        this.rims = rims
        this.tires = tires
    }

    val testStr = "wheels"
}

interface Engine {

    fun start()
}

class DieselEngine: Engine {

    @Inject
    constructor() {

    }

    override fun start() {
        System.out.println("start.........")
    }
}

class Tires {

}

class Rims {

}

@Component(modules = [PetrolEngineModule::class, WheelsModule::class])
interface CarComponent {
    fun getCar(): Car

    fun inject(mainActivity: MainActivity)
}

@Module
abstract class PetrolEngineModule {

    @Binds
    abstract fun providesPetrolEngine(dieselEngine: DieselEngine): Engine
}

@Module
class WheelsModule {

    @Provides
    fun providesTires(): Tires {
        return Tires()
    }

    @Provides
    fun providesRims(): Rims {
        return Rims()
    }

    @Provides
    fun providesWheels(rims: Rims, tires: Tires): Wheels {
        return Wheels(rims, tires)
    }
}

