using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CoralManager : MonoBehaviour
{

    [SerializeField] private GameObject coralParent;
    private List<Coral>[] _coralGroups;
	private int[] _callCounts;
	[SerializeField] private PostProcessGray grayscale;
    private float _timePerChange;
	private float _delay;
    private readonly int _tagLength = 5;
    
    private float _timeSinceLevelLoad;

    void Start()
    {
        _coralGroups = new List<Coral>[6];
        _callCounts = new int[6];
        for (int i = 0; i < 6; i++)
        {
            _coralGroups[i] = new List<Coral>();
        }
        foreach (Transform child in coralParent.transform){
            
            Coral coral = child.gameObject.GetComponent<Coral>();

            int index = (child.gameObject.tag[_tagLength] - '0') - 1;
            _coralGroups[index].Add(coral);
			
			if (_coralGroups[index].Count == 1)
            {
                coral.startColor();
            }
            
        }
		_timePerChange = grayscale.gameover / 21;
		_delay = 3*_timePerChange;
    }

    void Update()
    {
        _timeSinceLevelLoad += Time.deltaTime;
        float timePassed = _timeSinceLevelLoad - _delay;
		if (timePassed > 0)
        {
            for (int i = 0; i < 6; i++)
            {
                if (_callCounts[i] < 3 && timePassed >= (_callCounts[i] * 6 + i + 1) * _timePerChange)
                {
                    if (_coralGroups[i].Count > 0)
                    {
                        _coralGroups[i][0].changeColor(_coralGroups[i].Count);
                    }
                    _callCounts[i]++;
                }
            }
        }
		if(_timeSinceLevelLoad >= _timePerChange * 21 + 4.9f){
			for (int i = 0; i < 6; i++)
            {
				_coralGroups[i][0].startColor();
			}
			_timePerChange = _timePerChange * 21;
		}
    }


    
}
