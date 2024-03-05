using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class SceneController : MonoBehaviour {
	public int gridRows = 2;
	public int gridCols = 4;
	public const float offsetX = 1.5f;
	public const float offsetY = 1.4f;

	[SerializeField] private GameObject canvasCard;
	[SerializeField] private MemoryCard originalCard;
	[SerializeField] private Sprite[] images;
	[SerializeField] private TextMeshProUGUI scoreLabel;
	[SerializeField] private GameObject grid;
	[SerializeField] private GameObject smokePrefab;
	private List<MemoryCard> cards = new List<MemoryCard>();
	private GameObject _smoke1;
	private GameObject _smoke2;
	private MemoryCard _firstRevealed;
	private MemoryCard _secondRevealed;
	private int _score = 0;
	private bool updateRan;

	public bool canReveal {
		get {return _secondRevealed == null;}
	}

	void Awake(){
		if(PlayerPrefs.GetInt("rows") != 0){
			gridRows = PlayerPrefs.GetInt("rows", 2);
			gridCols = PlayerPrefs.GetInt("columns", 4);
		}
	}

	// Use this for initialization
	void Start() {
		Vector3 startPos = originalCard.transform.position;
		int[] numbers;
		// create shuffled list of cards
		if(gridRows == 2 && gridCols == 3){
			numbers = new int[]{0, 0, 1, 1, 2, 2};
			}
		else if(gridRows == 2 && gridCols == 4){
			numbers = new int[]{0, 0, 1, 1, 2, 2, 3, 3};
		}
		else if(gridRows == 2 && gridCols == 5){
			numbers = new int[]{0, 0, 1, 1, 2, 2, 3, 3, 4, 4};
		}
		else if(gridRows == 3 && gridCols == 4){
			numbers = new int[]{0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5};
		}
		else if(gridRows == 4 && gridCols == 4){
			numbers = new int[]{0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7};
		}
		else if(gridRows == 4 && gridCols == 5){
			numbers = new int[]{0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9};
		}
		else{
			numbers = null;
		}
		
		numbers = ShuffleArray(numbers);

		// place cards in a grid
		for (int i = 0; i < gridCols; i++) {
			for (int j = 0; j < gridRows; j++) {
				MemoryCard card;

				// use the original for the first grid space
				if (i == 0 && j == 0) {
					card = originalCard;
				} else {
					card = Instantiate(originalCard) as MemoryCard;
				}
				cards.Add(card);
				// next card in the list for each grid space
				int index = j * gridCols + i;
				int id = numbers[index];
				card.SetCard(id, images[id]);

				float posX = (offsetX * i) + startPos.x;
				float posY = -(offsetY * j) + startPos.y;
				card.transform.position = new Vector3(posX, posY, startPos.z);
			}
		}
	}

	private void Update(){
		if (Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}
		if(!updateRan){
			updateRan = true;
		}
	}

	// Knuth shuffle algorithm
	private int[] ShuffleArray(int[] numbers) {
		int[] newArray = numbers.Clone() as int[];
		for (int i = 0; i < newArray.Length; i++ ) {
			int tmp = newArray[i];
			int r = Random.Range(i, newArray.Length);
			newArray[i] = newArray[r];
			newArray[r] = tmp;
		}
		return newArray;
	}

	public void CardRevealed(MemoryCard card) {
		if (_firstRevealed == null) {
			_firstRevealed = card;
		} else {
			_secondRevealed = card;
			StartCoroutine(CheckMatch());
		}
	}
	
	private IEnumerator CheckMatch() {

		// increment score if the cards match
		if (_firstRevealed.id == _secondRevealed.id) {
			_score++;
			scoreLabel.text = "Score: " + _score;

			StartCoroutine(_firstRevealed.Shake(_firstRevealed.gameObject));
        	StartCoroutine(_secondRevealed.Shake(_secondRevealed.gameObject));

        	yield return new WaitForSeconds(0.5f);
			
			_smoke1 = Instantiate(smokePrefab) as GameObject;
			_smoke1.transform.position = _firstRevealed.transform.position;
			_smoke2 = Instantiate(smokePrefab) as GameObject;
			_smoke2.transform.position = _secondRevealed.transform.position;
			Destroy(_firstRevealed.gameObject);
			Destroy(_secondRevealed.gameObject);
			yield return new WaitForSeconds(0.5f);
			Destroy(_smoke1);
			Destroy(_smoke2);

			if (_score == (gridRows * gridCols) / 2) {
        	SceneManager.LoadScene(1);
    		}
		}

		// otherwise turn them back over after .5s pause
		else {
			yield return new WaitForSeconds(.5f);

			_firstRevealed.Unreveal();
			_secondRevealed.Unreveal();
		}
		
		_firstRevealed = null;
		_secondRevealed = null;
	}

	public void Restart() {
		if(updateRan){
		SceneManager.LoadScene("Scene");
		}
	}

	public void SetSize(int row, int col){
		gridRows = row;
		gridCols = col;
	}

	public void SetupGridLayout()
	{
		GameObject c_card;
		GameObject grid = GameObject.Find("GridLayout").gameObject;
		GridLayoutGroup glg = grid.GetComponent<GridLayoutGroup>();

		glg.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
		glg.constraintCount = gridCols;
		glg.cellSize.Set(150f, 150f);
		glg.spacing.Set(5f, 5f);

        for (int i = 0; i < gridCols; i++)
		{
			for (int j = 0; j < gridRows-1; j++)
			{
				c_card = Instantiate(canvasCard) as GameObject;
				c_card.transform.SetParent(grid.transform);
			}
		}
	}
}
