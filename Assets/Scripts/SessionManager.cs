using System.Runtime.CompilerServices;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    // ----------------------- Singleton -----------------------
    // Singleton Instance
    public static SessionManager instance { get; private set; }

    // ----------------------- Services -----------------------
    // public APIService service { get; private set; }

    // ----------------------- Data Base Variables (Getters and Setters) -----------------------
    // Session Configurations
    public float SessionTotalTime { get; set; } // Total time of the session in seconds
    public int EnemiesTotal { get; set; } // Total number of enemies to spawn during the session
    public float EnemiesSpawnRate { get; set; } // Time interval between enemy spawns in seconds
    public float EnemiesVelocity { get; set; } // Velocity of the enemies during the session

    // Session Results
    // Scores
    public int LeftFinalScore { get; set; } // Final score achieved by the Pacient in left arm
    public int LeftRecordScore { get; set; } // Highest score achieved by the Pacient in left arm
    public int RightFinalScore { get; set; } // Final score achieved by the Pacient in right arm
    public int RightRecordScore { get; set; } // Highest score achieved by the Pacient in right arm
    public float TotalAccuracy { get; set; } // Percentage of enemies hit vs total enemies spawned
    // Patient Status
    public float LeftMinAngle { get; set; } // Minimum left arm angle reached by the Pacient during the session
    public float LeftMaxAngle { get; set; } // Maximum left arm angle reached by the Pacient during the session
    public float RightMinAngle { get; set; } // Minimum right arm angle reached by the Pacient during the session
    public float RightMaxAngle { get; set; } // Maximum right arm angle reached by the Pacient during the session
    public int LastRange { get; set; } // The range of movement of the Pacient in the last session
    public int ActualRange { get; set; } // The range of movement of the Pacient in the actual session
    public float LeftAngleProgress { get; set; } // Percentage of improvement in left angle range
    public float RightAngleProgress { get; set; } // Percentage of improvement in right angle range

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            //service = new APIService();
        }
    }
}
