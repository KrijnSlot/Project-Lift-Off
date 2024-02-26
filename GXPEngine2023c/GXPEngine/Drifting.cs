/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class DriftingCar
{
    public Vector2 Position { get; private set; }
    public Vector2 Velocity { get; private set; }
    public float Rotation { get; private set; } // In degrees
    public float TurnSpeed { get; set; } = 0.1f;
    public float MaxSpeed { get; set; } = 10.0f;
    public float Acceleration { get; set; } = 0.2f;
    public float Deceleration { get; set; } = 0.05f;
    public float DriftIntensity { get; set; } = 0.3f;

    private float currentSpeed = 0f;
    private float driftAngle = 0f;

    public void Update(float accelerationInput, float steeringInput, float deltaTime)
    {
        HandleAcceleration(accelerationInput, deltaTime);
        HandleSteering(steeringInput, deltaTime);
        ApplyPhysics(deltaTime);
        UpdatePosition(deltaTime);
    }

    private void HandleAcceleration(float input, float deltaTime)
    {
        // Accelerate or decelerate the car
        currentSpeed += Acceleration * input * deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, -MaxSpeed, MaxSpeed);

        // Apply deceleration if no input is given (simulate friction)
        if (input == 0)
        {
            currentSpeed = Approach(currentSpeed, 0, Deceleration * deltaTime);
        }
    }

    private void HandleSteering(float input, float deltaTime)
    {
        if (currentSpeed != 0)
        {
            // Calculate the turn amount based on input and current speed
            float turnAmount = TurnSpeed * input * deltaTime * (currentSpeed / MaxSpeed);
            Rotation += turnAmount;
            driftAngle = turnAmount * DriftIntensity;
        }
    }

    private void ApplyPhysics(float deltaTime)
    {
        // Modify the velocity based on the current rotation and drift angle
        float tractionFactor = 1.0f - (Mathf.Abs(driftAngle) / 90.0f); // Reduce traction when drifting
        Velocity += Vector2.Rotate(Vector2.Up, Rotation + driftAngle) * currentSpeed * tractionFactor * deltaTime;
    }

    private void UpdatePosition(float deltaTime)
    {
        // Update the position based on the velocity
        Position += Velocity * deltaTime;
       
        // Reduce the velocity over time (simulate drag)
        Velocity = Velocity * (1f - Deceleration * deltaTime);
    }

    private float Approach(float start, float end, float shift)
    {
        if (start < end)
            return Mathf.Min(start + shift, end);
        else
            return Mathf.Max(start - shift, end);
    }
}

public struct Vector2
{
    float x;
    float y; WeakReference

    public Vector2  (float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public static Vector2 Rotate(Vector2 v, float degrees)
    {
        
    }

    public static readonly Vector2 Up = new Vector2(0, 1);
    // ...
}



*/